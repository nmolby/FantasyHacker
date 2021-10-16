using System;
using System.Text.Json;
using System.Linq;
using FantasyHacker.Interface;
using FantasyHacker.Helper;
using System.Threading.Tasks;
using FantasyHacker.Model;
using System.Collections.Generic;
using Extreme.Statistics;

namespace FantasyHacker
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var avgEraCorrelations = await SimulateDay(new DateTime(2021, 10, 01), new AvgEraAlgorithm());
            var randomCorrelations = await SimulateDay(new DateTime(2021, 10, 01), new RandomAlgorithm());
            Console.WriteLine($"Batting Average & ERA Correlation: {string.Join(',', avgEraCorrelations)}");
            Console.WriteLine($"Random Correlation: {string.Join(',', randomCorrelations)}");
            Console.WriteLine($"Batting Average & ERA Correlation Average: {avgEraCorrelations.Average()}");
            Console.WriteLine($"Random Correlation Average: {randomCorrelations.Average()}");
        }

        static async Task<List<double>> SimulateDay(DateTime day, IAlgorithm<MLBPlayer> algorithm)
        {
            var mlbApiHelper = new MLBAPIHelper();
            var schedule = await mlbApiHelper.GetSchedule(day);
            var playersByGame = new Dictionary<ScheduleResponse.Game, List<MLBPlayer>>();
            var correlations = new List<double>();
            foreach (var date in schedule.Dates)
            {
                foreach (var game in date.Games)
                {
                    playersByGame.Add(game, (await RetrievePlayerInfo(game.GamePk, day.Year)).Where(x => x.IsActivePlayer()).ToList());
                }
            }

            foreach(var keyValuePair in playersByGame)
            {
                var playerRanks = algorithm.RankPlayers(keyValuePair.Value);
                var predictedPlayerOrder = playerRanks.ToList().OrderByDescending(x => x.Value).Select(x => x.Key).ToList();
                var actualPlayerOrder = keyValuePair.Value.OrderByDescending(x => x.Score()).ToList();
                var correlation = Stats.KendallTau(predictedPlayerOrder, actualPlayerOrder);
                correlations.Add(correlation);
            }

            return correlations;
        }

        static async Task<List<MLBPlayer>> RetrievePlayerInfo(int gameId, int season)
        {
            var mlbApiHelper = new MLBAPIHelper();
            var boxScore = await mlbApiHelper.GetBoxScore(gameId);
            var players = new List<MLBPlayer>();

            foreach(IFantasyPlayer player in boxScore.Score())
            {
                var newPlayer = new MLBPlayer(player.Person.Id);
                newPlayer.InjestBoxScore(boxScore);
                players.Add(newPlayer);
            }
            var seasonStats = await mlbApiHelper.GetPeopleStats(players.Select(x => x.PlayerId), season);

            foreach(var player in players)
            {
                player.InjestSeasonStats(seasonStats);
            }
            return players;
        } 
    }
}
