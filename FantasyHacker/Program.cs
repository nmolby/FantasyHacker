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
            var randAlgEval = await EvaluateAlgorithm(new RandomAlgorithm(), 10);
            var batAvgEraEval = await EvaluateAlgorithm(new AvgEraAlgorithm(), 10);
            Console.WriteLine($"Random Algorithm Average: {randAlgEval.averageCorrelation}. Confidence Interval: {randAlgEval.confidenceInterval}. Std. Deviation: {randAlgEval.stdCorrelation}");
            Console.WriteLine($"Batting Average ERA Average: {batAvgEraEval.averageCorrelation}. Confidence Interval: {batAvgEraEval.confidenceInterval}. Std. Deviation: {batAvgEraEval.stdCorrelation}");
        }

        static async Task<AlgorithmEvaluation<MLBPlayer>> EvaluateAlgorithm(IAlgorithm<MLBPlayer> algorithm, int sampleSize = 50)
        {
            var eval = new AlgorithmEvaluation<MLBPlayer>(algorithm);
            var mlbApiHelper = new MLBAPIHelper();
            var datesOfInterest = new List<ScheduleResponse.Date>();
            

            List<Task<ScheduleResponse.Schedule>> schedulesOfInterest = new List<Task<ScheduleResponse.Schedule>>() {
                mlbApiHelper.GetSchedule(2021),
                mlbApiHelper.GetSchedule(2020),
                mlbApiHelper.GetSchedule(2019)
            };

            while(schedulesOfInterest.Any())
            {
                var schedule = await Task.WhenAny(schedulesOfInterest);
                schedulesOfInterest.Remove(schedule);
                datesOfInterest.AddRange((await schedule).Dates);
            }

            var rnd = new Random();
            var randomDates = datesOfInterest.OrderBy(x => rnd.Next()).Take(sampleSize);

            foreach(var date in randomDates)
            {
                var playerRanksByGame = await RankPlayersByGame(date, algorithm);
                var correlations = CalculateCorrelations(playerRanksByGame);

                eval.AddCorrelations(correlations);
            }

            return eval;
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

        static async Task<Dictionary<ScheduleResponse.Game, Dictionary<MLBPlayer, decimal>>> RankPlayersByGame(ScheduleResponse.Date day, IAlgorithm<MLBPlayer> algorithm)
        {
            var playerRanksByGame = new Dictionary<ScheduleResponse.Game, Dictionary<MLBPlayer, decimal>>();

            foreach (var game in day.Games)
            {
                var players = (await RetrievePlayerInfo(game.GamePk, game.GameDate.Year)).Where(x => x.IsActivePlayer());
                var playerRanks = algorithm.RankPlayers(players);
                playerRanksByGame.Add(game, playerRanks);
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
