using System;
using System.Text.Json;
using FantasyHacker.Interface;
using FantasyHacker.Helper;
using System.Threading.Tasks;

namespace FantasyHacker
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var mlbApiHelper = new MLBAPIHelper();
            var schedule = await mlbApiHelper.GetSchedule(new DateTime(2021, 09, 12));
            foreach(var date in schedule.Dates)
            {
                foreach(var game in date.Games)
                {
                    Console.WriteLine($"GAME: {game.Teams.Away.Team.Name} at {game.Teams.Home.Team.Name} -------------------------");
                    var boxScore = await mlbApiHelper.GetBoxScore(game.GamePk);
                    foreach (IFantasyPlayer player in boxScore.Score())
                    {
                        if (player.Score() != 0)
                        {
                            Console.WriteLine($"Player {player.Person.FullName} : {player.Score()}");
                        }
                    }
                }
            }


        }
    }
}
