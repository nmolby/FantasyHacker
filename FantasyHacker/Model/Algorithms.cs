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

    //Uses the best model calculated by including all of the season stats and using
    //stepwise / backward / forward selection (they all have the same result)
    //results in an r^2 of 0.0838
    //ONLY WORKS FOR BATTERS
    public class SlgRunsAlgorithm : IAlgorithm<MLBPlayer>
    {
        public Dictionary<MLBPlayer, decimal> RankPlayers(IEnumerable<MLBPlayer> players)
        {
            var myDict = new Dictionary<MLBPlayer, decimal>();

            foreach (var player in players)
            {
                decimal rank;
                if (player.Position.Code != "1")
                {
                    decimal slugging;
                    if(!decimal.TryParse(player.SeasonStatsBeforeGame.Batting.Slg, out slugging))
                    {
                        slugging = 0;
                    }

                    rank = slugging * 13.529207m + player.SeasonStatsBeforeGame.Batting.Runs * 0.044866m;
                }
                else
                {
                    rank = 0;
                }
                myDict.Add(player, rank);
            }

            return myDict;
        }
    }
}

