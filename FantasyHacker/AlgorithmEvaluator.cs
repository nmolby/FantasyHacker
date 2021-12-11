using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FantasyHacker.Helper;
using FantasyHacker.Interface;
using FantasyHacker.Model;
using Extreme.Statistics;
using System.Linq;

namespace FantasyHacker
{
    public class AlgorithmEvaluator
    {
        private MLBAPIHelper apiHelper = new();

        public async Task<AlgorithmEvaluation<MLBPlayer>> EvaluateAlgorithm(IAlgorithm<MLBPlayer> algorithm, int sampleSize = 50, bool battersOnly = false)
        {
            var eval = new AlgorithmEvaluation<MLBPlayer>(algorithm);

            var randomDates = await apiHelper.GetRandomDates(sampleSize);

            foreach (var date in randomDates)
            {
                var playerRanksByGame = await RankPlayersByGame(date, algorithm, battersOnly);
                var correlations = CalculateCorrelations(playerRanksByGame);

                eval.AddCorrelations(correlations);
            }

            return eval;
        }

        private Dictionary<ScheduleResponse.Game, decimal> CalculateCorrelations(Dictionary<ScheduleResponse.Game, Dictionary<MLBPlayer, decimal>> playerRanksByGame)
        {
            var correlationsByGame = new Dictionary<ScheduleResponse.Game, decimal>();

            foreach (var gamePlayerRanks in playerRanksByGame)
            {
                var game = gamePlayerRanks.Key;
                var playerRanks = gamePlayerRanks.Value;
                var predictedPlayerOrder = playerRanks.ToList().OrderByDescending(x => x.Value).Select(x => x.Key).ToList();
                var actualPlayerOrder = playerRanks.Select(x => x.Key).OrderByDescending(x => x.Score()).ToList();
                var correlation = Stats.KendallTau(predictedPlayerOrder, actualPlayerOrder);
                correlationsByGame.Add(game, (decimal)correlation);
            }

            return correlationsByGame;
        }

        private async Task<Dictionary<ScheduleResponse.Game, Dictionary<MLBPlayer, decimal>>> RankPlayersByGame(DateTime startDay, DateTime endDay, IAlgorithm<MLBPlayer> algorithm)
        {
            var mlbApiHelper = new MLBAPIHelper();
            var schedule = await mlbApiHelper.GetSchedule(startDay, endDay);
            var playerRanksByGame = new Dictionary<ScheduleResponse.Game, Dictionary<MLBPlayer, decimal>>();

            foreach (var date in schedule.Dates)
            {
                foreach (var game in date.Games)
                {
                    var players = (await apiHelper.RetrievePlayerInfo(game.GamePk, date.DateValue)).Where(x => x.IsActivePlayer());
                    var playerRanks = algorithm.RankPlayers(players);
                    playerRanksByGame.Add(game, playerRanks);
                }
            }
            return playerRanksByGame;
        }

        private async Task<Dictionary<ScheduleResponse.Game, Dictionary<MLBPlayer, decimal>>> RankPlayersByGame(DateTime day, IAlgorithm<MLBPlayer> algorithm)
        {
            var mlbApiHelper = new MLBAPIHelper();
            var schedule = await mlbApiHelper.GetSchedule(day);
            var playerRanksByGame = new Dictionary<ScheduleResponse.Game, Dictionary<MLBPlayer, decimal>>();
            foreach (var date in schedule.Dates)
            {
                foreach (var game in date.Games)
                {
                    var players = (await apiHelper.RetrievePlayerInfo(game.GamePk, day)).Where(x => x.IsActivePlayer());
                    var playerRanks = algorithm.RankPlayers(players);
                    playerRanksByGame.Add(game, playerRanks);
                }
            }
            return playerRanksByGame;

        }

        private async Task<Dictionary<ScheduleResponse.Game, Dictionary<MLBPlayer, decimal>>> RankPlayersByGame(ScheduleResponse.Date day, IAlgorithm<MLBPlayer> algorithm, bool battersOnly)
        {
            var playerRanksByGame = new Dictionary<ScheduleResponse.Game, Dictionary<MLBPlayer, decimal>>();

            foreach (var game in day.Games)
            {
                var players = (await apiHelper.RetrievePlayerInfo(game.GamePk, game.GameDate)).Where(x => x.IsActivePlayer());
                if (battersOnly)
                    players = players.Where(x => !x.IsPitcher());
                var playerRanks = algorithm.RankPlayers(players);
                playerRanksByGame.Add(game, playerRanks);
            }
            return playerRanksByGame;

        }

    }
}

