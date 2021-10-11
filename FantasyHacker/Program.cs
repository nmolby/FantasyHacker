using System;
using System.Text.Json;
using System.Linq;
using FantasyHacker.Interface;
using FantasyHacker.Helper;
using System.Threading.Tasks;
using FantasyHacker.Model;
using System.Collections.Generic;

namespace FantasyHacker
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var playerInfo = await RetrievePlayerInfo(632580);
        }

        static async Task SimulateDay(DateTime day, Algorithm algorithm)
        {
            var mlbApiHelper = new MLBAPIHelper();
            var schedule = await mlbApiHelper.GetSchedule(day);
            var players = new List<IFantasyPlayer>();

            foreach (var date in schedule.Dates)
            {
                foreach (var game in date.Games)
                {
                    var boxScore = await mlbApiHelper.GetBoxScore(game.GamePk);
                    players.AddRange(boxScore.Score());
                }
            }
        }

        static async Task<List<MLBPlayer>> RetrievePlayerInfo(int gameId)
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
            var season = (await mlbApiHelper.GetLiveFeed(gameId)).GameData.Game.Season;
            var seasonStats = await mlbApiHelper.GetPeopleStats(players.Select(x => x.PlayerId), season);

            foreach(var player in players)
            {
                player.InjestSeasonStats(seasonStats);
            }
            return players;
        } 
    }
}
