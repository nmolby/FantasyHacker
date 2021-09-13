using System;
using System.Text.Json;
using FantasyHacker;


namespace FantasyHacker
{
    class Program
    {
        static void Main(string[] args)
        {
            string myJsonResponse = System.IO.File.ReadAllText(@"/Users/nmolby/Projects/FantasyHacker/FantasyHacker/BoxScore.json");
            BoxScore.BoxScore boxScore = JsonSerializer.Deserialize<BoxScore.BoxScore>(myJsonResponse);

            foreach(Interface.IFantasyPlayer player in boxScore.Score())
            {
                if(player.Score() != 0)
                {
                    Console.WriteLine($"Player {player.Person.FullName} : {player.Score()}");
                }
            }
        }
    }
}
