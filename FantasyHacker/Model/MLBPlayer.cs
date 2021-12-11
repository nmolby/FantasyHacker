using System;
using System.Collections.Generic;
using FantasyHacker.Interface;
using System.Linq;
using System.Diagnostics.CodeAnalysis;
using CsvHelper;
using System.IO;
using System.Globalization;

namespace FantasyHacker.Model
{
    /// <summary>
    /// Represents the stats of a specific player for a specific game. Also can injest season stats of that player for the season of the game.
    /// </summary>
    public class MLBPlayer : IFantasyPlayer, IComparable<MLBPlayer>
    {
        public int PlayerId { get; set; }
        public int GameId { get; set; }
        public DateTime DateOfGame { get; private set; }
        public BoxScoreResponse.BoxScore BoxScore { get; private set; }
        public BoxScoreResponse.Player PlayerGameStats { get; private set; }
        public PersonResponse.Person PersonResponseRoot { get; private set; }
        public List<FantasyHacker.PersonResponse.Split> SeasonStatsByGame { get => PersonResponseRoot.Stats.Where(x => x.Type.DisplayName == "gameLog").FirstOrDefault().Splits; }
        public BoxScoreResponse.SeasonStats SeasonStatsBeforeGame { get => PlayerGameStats.SeasonStats; }
        public BoxScoreResponse.Person Person { get => PlayerGameStats?.Person; }
        public BoxScoreResponse.Position Position { get => PlayerGameStats?.Position; }
        public BoxScoreResponse.Stats GameStats { get => PlayerGameStats?.GameStats; }

        public MLBPlayer(int playerId, DateTime dateOfGame)
        {
            PlayerId = playerId;
            DateOfGame = dateOfGame;
        }

        /// <summary>
        /// Ingest a game box score and extracts and stores the stats for this player.
        /// </summary>
        /// <param name="boxScore"></param>
        public void IngestBoxScore(BoxScoreResponse.BoxScore boxScore, int GameId)
        {
            this.GameId = GameId;
            BoxScore = boxScore;
            foreach (var team in new List<ITeamGame>() { boxScore.Teams.Away, boxScore.Teams.Home})
            {
                foreach (var player in team.Players.Values)
                {
                    if(player.Person.Id == PlayerId)
                    {
                        PlayerGameStats = player;
                        break;
                    }
                }
                if(PlayerGameStats != null)
                {
                    break;
                }
            }
            if(PlayerGameStats == null)
            {
                throw new Exception("Player not found in box score");
            }
        }

        public void IngestSeasonStats(PersonResponse.PersonResponseRoot personResponseRoot)
        {
            PersonResponseRoot = personResponseRoot.People.Where(x => x.Id == PlayerId).FirstOrDefault();
            if(PersonResponseRoot == null)
            {
                throw new Exception("Player not found in season stats");
            }
        }

        public decimal Score()
        {
            return PlayerGameStats.Score();
        }

        public bool IsActivePlayer()
        {
            if(PlayerGameStats.GameStatus.IsOnBench || (GameStats.Batting.AtBats == 0 && GameStats.Pitching.BattersFaced == 0))
            {
                return false;
            } else
            {
                return true;
            }
        }

        public bool IsPitcher()
        {
            return Position.Code == "1";
        }

        public int CompareTo(MLBPlayer other)
        {
            if(PlayerId == other.PlayerId)
            {
                return 0;
            } else if (PlayerId > other.PlayerId)
            {
                return 1;
            } else
            {
                return -1;
            }
        }

        public MlbStats GetStats(int pastGames = -1)
        {
            if(pastGames == -1)
            {
                return GetSeasonStats();
            } else
            {
                if(SeasonStatsByGame.Count == 0 || SeasonStatsByGame.Where(x => x.Date <= DateOfGame).Count() == 0)
                {
                    Console.WriteLine($"BRO IT BROKE FOR PLAYER {PlayerId} for {DateOfGame}");
                }
                var pastXGames = SeasonStatsByGame.Where(x => x.Date <= DateOfGame).OrderBy(x => x.Date).Take(pastGames);
                var sbCount = pastXGames.Select(x => x.Stat.StolenBases).Sum();
                var csCount = pastXGames.Select(x => x.Stat.CaughtStealing).Sum();
                var hitCount = pastXGames.Select(x => x.Stat.Hits).Sum();
                var paCount = pastXGames.Select(x => x.Stat.PlateAppearances).Sum();
                var abCount = pastXGames.Select(x => x.Stat.AtBats).Sum();
                var totalBaseCount = pastXGames.Select(x => x.Stat.TotalBases).Sum();
                var runCount = pastXGames.Select(x => x.Stat.Runs).Sum();
                var rbiCount = pastXGames.Select(x => x.Stat.Rbi).Sum();
                var hrCount = pastXGames.Select(x => x.Stat.HomeRuns).Sum();
                var walkCount = pastXGames.Select(x => x.Stat.BaseOnBalls).Sum();
                var hbpCount = pastXGames.Select(x => x.Stat.HitByPitch).Sum();
                var sacFlyCount = pastXGames.Select(x => x.Stat.SacFlies).Sum();
                var doubleCount = pastXGames.Select(x => x.Stat.Doubles).Sum();
                var tripleCount = pastXGames.Select(x => x.Stat.Triples).Sum();
                var soCount = pastXGames.Select(x => x.Stat.StrikeOuts).Sum();

                var singleCount = hitCount - hrCount - tripleCount - doubleCount;

                var ba = abCount == 0 ? 0 : ((float) hitCount) / abCount;
                var obp = abCount + walkCount + hbpCount + sacFlyCount == 0 ? 0 : ((float) hitCount + walkCount + hbpCount) / (abCount + walkCount + hbpCount + sacFlyCount);
                var slg = abCount == 0 ? 0 : ((float) singleCount + 2 * doubleCount + 3 * tripleCount + 4 * hrCount) / abCount;
                var bapip = abCount - soCount - hrCount + sacFlyCount == 0 ? 0 : ((float) hitCount - hrCount) / (abCount - soCount - hrCount + sacFlyCount);
                var sbPercent = sbCount + csCount == 0 ? 0 : sbCount / ((float) sbCount + csCount);

                return new MlbStats
                {
                    BA = ba.ToString(),
                    OBP = obp.ToString(),
                    SLG = slg.ToString(),
                    BAPIP = bapip.ToString(),
                    SBPercentage = sbPercent.ToString(),
                    SBCount = sbCount,
                    HitCount = hitCount,
                    PACount = paCount,
                    TotalBaseCount = totalBaseCount,
                    RunCount = runCount,
                    RBICount = rbiCount,
                    HomeRunCount = hrCount,
                    WalkCount = walkCount,
                    HBPCount = hbpCount,
                    AtBatCount = abCount,
                    SacFlyCount = sacFlyCount,
                    DoubleCount = doubleCount,
                    TripleCount = tripleCount,
                    SOCount = soCount,
                    CaughtStealingCount = csCount
                };
            }
        }

        private MlbStats GetSeasonStats()
        {
            var stats = new MlbStats()
            {
                BA = SeasonStatsBeforeGame.Batting.Avg,
                OBP = SeasonStatsBeforeGame.Batting.Obp,
                SLG = SeasonStatsBeforeGame.Batting.Slg,
                BAPIP = SeasonStatsBeforeGame.Batting.Babip,
                SBPercentage = SeasonStatsBeforeGame.Batting.StolenBasePercentage,
                SBCount = SeasonStatsBeforeGame.Batting.StolenBases,
                HitCount = SeasonStatsBeforeGame.Batting.Hits,
                PACount = SeasonStatsBeforeGame.Batting.PlateAppearances,
                TotalBaseCount = SeasonStatsBeforeGame.Batting.TotalBases,
                RunCount = SeasonStatsBeforeGame.Batting.Runs,
                RBICount = SeasonStatsBeforeGame.Batting.Rbi,
                HomeRunCount = SeasonStatsBeforeGame.Batting.HomeRuns,
                WalkCount = SeasonStatsBeforeGame.Batting.BaseOnBalls,
                HBPCount = SeasonStatsBeforeGame.Batting.HitByPitch,
                AtBatCount = SeasonStatsBeforeGame.Batting.AtBats,
                SacFlyCount = SeasonStatsBeforeGame.Batting.SacFlies,
                DoubleCount = SeasonStatsBeforeGame.Batting.Doubles,
                TripleCount = SeasonStatsBeforeGame.Batting.Triples,
                SOCount = SeasonStatsBeforeGame.Batting.StrikeOuts,
                CaughtStealingCount = SeasonStatsBeforeGame.Batting.CaughtStealing

            };

            return stats;
        }


    }
}

