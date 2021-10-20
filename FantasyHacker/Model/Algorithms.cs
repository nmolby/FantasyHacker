using System;
using System.Collections.Generic;
using System.Linq;
using FantasyHacker.Interface;
namespace FantasyHacker.Model
{
    public class AvgEraAlgorithm : IAlgorithm <MLBPlayer>
    {
        public Dictionary<MLBPlayer, decimal> RankPlayers(IEnumerable<MLBPlayer> players)
        {
            var myDict = new Dictionary<MLBPlayer, decimal>();

            foreach(var player in players)
            {
                decimal rank;
                if(player.Position.Code == "1")
                {
                    var success = decimal.TryParse(player.SeasonStatsBeforeGame.Pitching.Era, out rank);
                    //to normalize ERA against batting avg, we will take the reciprocal
                    //this puts an era of 3 equal to a batting avg of .333 and an era of 2 equal to a avg of .5
                    if(success && rank != 0)
                    {
                        rank = 1 / rank;
                    } else if(success && rank == 0)
                    {
                        rank = 1;
                    } else
                    {
                        rank = 0;
                    }
                } else
                {
                    var success = decimal.TryParse(player.SeasonStatsBeforeGame.Batting.Avg, out rank);
                    rank = success ? rank : 0;
                }
                myDict.Add(player, rank);
            }

            return myDict;
        }
    }

    public class RandomAlgorithm : IAlgorithm<MLBPlayer>
    {
        public Dictionary<MLBPlayer, decimal> RankPlayers(IEnumerable<MLBPlayer> players)
        {
            var myDict = new Dictionary<MLBPlayer, decimal>();

            foreach (var player in players)
            {
                decimal rank = (decimal) new Random().NextDouble();
                myDict.Add(player, rank);
            }

            return myDict;
        }
    }
}

