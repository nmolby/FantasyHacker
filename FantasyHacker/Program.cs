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
            var playerRanksAvgEraAlg = await RankPlayersByGame(new DateTime(2021, 10, 01), new AvgEraAlgorithm());
            var playerRanksRandAlg = await RankPlayersByGame(new DateTime(2021, 10, 01), new RandomAlgorithm());
            var corByGameAvgEraAlg = CalculateCorrelations(playerRanksAvgEraAlg);
            var corByGameRandAlg = CalculateCorrelations(playerRanksRandAlg);
            Console.WriteLine($"Batting Average & ERA Correlation: {string.Join(',', corByGameAvgEraAlg.Values.ToList())}");
            Console.WriteLine($"Random Correlation: {string.Join(',', corByGameRandAlg.Values.ToList())}");
            Console.WriteLine($"Batting Average & ERA Correlation Average: {corByGameAvgEraAlg.Values.ToList().Average()}");
            Console.WriteLine($"Random Correlation Average: {corByGameRandAlg.Values.ToList().Average()}");
        }

        static Dictionary<ScheduleResponse.Game, decimal> CalculateCorrelations(Dictionary<ScheduleResponse.Game, Dictionary<MLBPlayer, decimal>> playerRanksByGame)
        {
            var correlationsByGame = new Dictionary<ScheduleResponse.Game, decimal>();

            foreach (var gamePlayerRanks in playerRanksByGame)
            {
                var game = gamePlayerRanks.Key;
                var playerRanks = gamePlayerRanks.Value;
                var predictedPlayerOrder = playerRanks.ToList().OrderByDescending(x => x.Value).Select(x => x.Key).ToList();
                var actualPlayerOrder = playerRanks.Select(x => x.Key).OrderByDescending(x => x.Score()).ToList();
                var correlation = Stats.KendallTau(predictedPlayerOrder, actualPlayerOrder);
                correlationsByGame.Add(game, (decimal) correlation);
            }

            return correlationsByGame;
        }

        static async Task<Dictionary<ScheduleResponse.Game, Dictionary<MLBPlayer, decimal>>> RankPlayersByGame(DateTime startDay, DateTime endDay, IAlgorithm<MLBPlayer> algorithm)
        {
            var mlbApiHelper = new MLBAPIHelper();
            var schedule = await mlbApiHelper.GetSchedule(startDay, endDay);
            var playerRanksByGame = new Dictionary<ScheduleResponse.Game, Dictionary<MLBPlayer, decimal>>();

            foreach (var date in schedule.Dates)
            {
                foreach (var game in date.Games)
                {
                    var players = (await RetrievePlayerInfo(game.GamePk, startDay.Year)).Where(x => x.IsActivePlayer());
                    var playerRanks = algorithm.RankPlayers(players);
                    playerRanksByGame.Add(game, playerRanks);
                }
            }
            return playerRanksByGame;
        }

        static async Task<Dictionary<ScheduleResponse.Game, Dictionary<MLBPlayer, decimal>>> RankPlayersByGame(DateTime day, IAlgorithm<MLBPlayer> algorithm)
        {
            var mlbApiHelper = new MLBAPIHelper();
            var schedule = await mlbApiHelper.GetSchedule(day);
            var playerRanksByGame = new Dictionary<ScheduleResponse.Game, Dictionary<MLBPlayer, decimal>>();
            foreach (var date in schedule.Dates)
            {
                foreach (var game in date.Games)
                {
                    var players = (await RetrievePlayerInfo(game.GamePk, day.Year)).Where(x => x.IsActivePlayer());
                    var playerRanks = algorithm.RankPlayers(players);
                    playerRanksByGame.Add(game, playerRanks);
                }
            }
            return playerRanksByGame;

        }

        static async Task<List<MLBPlayer>> RetrievePlayerInfo(int gameId, int season)
        {
            var mlbApiHelper = new MLBAPIHelper();
            var boxScore = await mlbApiHelper.GetBoxScore(gameId);
            var players = new List<MLBPlayer>();

            foreach(IFantasyPlayer player in boxScore.GetPlayers())
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
