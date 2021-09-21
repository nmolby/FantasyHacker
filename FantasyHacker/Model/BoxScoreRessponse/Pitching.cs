using System.Collections.Generic;
using System.Text.Json.Serialization;
using FantasyHacker.Interface;

namespace FantasyHacker.BoxScoreResponse
{
    public class Pitching : IFantasyScoreable
    {
        [JsonPropertyName("groundOuts")]
        public int GroundOuts { get; set; }

        [JsonPropertyName("airOuts")]
        public int AirOuts { get; set; }

        [JsonPropertyName("runs")]
        public int Runs { get; set; }

        [JsonPropertyName("doubles")]
        public int Doubles { get; set; }

        [JsonPropertyName("triples")]
        public int Triples { get; set; }

        [JsonPropertyName("homeRuns")]
        public int HomeRuns { get; set; }

        [JsonPropertyName("strikeOuts")]
        public int StrikeOuts { get; set; }

        [JsonPropertyName("baseOnBalls")]
        public int BaseOnBalls { get; set; }

        [JsonPropertyName("intentionalWalks")]
        public int IntentionalWalks { get; set; }

        [JsonPropertyName("hits")]
        public int Hits { get; set; }

        [JsonPropertyName("hitByPitch")]
        public int HitByPitch { get; set; }

        [JsonPropertyName("atBats")]
        public int AtBats { get; set; }

        [JsonPropertyName("obp")]
        public string Obp { get; set; }

        [JsonPropertyName("caughtStealing")]
        public int CaughtStealing { get; set; }

        [JsonPropertyName("stolenBases")]
        public int StolenBases { get; set; }

        [JsonPropertyName("stolenBasePercentage")]
        public string StolenBasePercentage { get; set; }

        [JsonPropertyName("era")]
        public string Era { get; set; }

        [JsonPropertyName("inningsPitched")]
        public string InningsPitched { get; set; }

        [JsonPropertyName("saveOpportunities")]
        public int SaveOpportunities { get; set; }

        [JsonPropertyName("earnedRuns")]
        public int EarnedRuns { get; set; }

        [JsonPropertyName("whip")]
        public string Whip { get; set; }

        [JsonPropertyName("battersFaced")]
        public int BattersFaced { get; set; }

        [JsonPropertyName("outs")]
        public int Outs { get; set; }

        [JsonPropertyName("completeGames")]
        public int CompleteGames { get; set; }

        [JsonPropertyName("shutouts")]
        public int Shutouts { get; set; }

        [JsonPropertyName("hitBatsmen")]
        public int HitBatsmen { get; set; }

        [JsonPropertyName("balks")]
        public int Balks { get; set; }

        [JsonPropertyName("wildPitches")]
        public int WildPitches { get; set; }

        [JsonPropertyName("pickoffs")]
        public int Pickoffs { get; set; }

        [JsonPropertyName("groundOutsToAirouts")]
        public string GroundOutsToAirouts { get; set; }

        [JsonPropertyName("rbi")]
        public int Rbi { get; set; }

        [JsonPropertyName("runsScoredPer9")]
        public string RunsScoredPer9 { get; set; }

        [JsonPropertyName("homeRunsPer9")]
        public string HomeRunsPer9 { get; set; }

        [JsonPropertyName("inheritedRunners")]
        public int InheritedRunners { get; set; }

        [JsonPropertyName("inheritedRunnersScored")]
        public int InheritedRunnersScored { get; set; }

        [JsonPropertyName("catchersInterference")]
        public int CatchersInterference { get; set; }

        [JsonPropertyName("sacBunts")]
        public int SacBunts { get; set; }

        [JsonPropertyName("sacFlies")]
        public int SacFlies { get; set; }

        [JsonPropertyName("note")]
        public string Note { get; set; }

        [JsonPropertyName("gamesPlayed")]
        public int GamesPlayed { get; set; }

        [JsonPropertyName("gamesStarted")]
        public int GamesStarted { get; set; }

        [JsonPropertyName("flyOuts")]
        public int FlyOuts { get; set; }

        [JsonPropertyName("numberOfPitches")]
        public int NumberOfPitches { get; set; }

        [JsonPropertyName("wins")]
        public int Wins { get; set; }

        [JsonPropertyName("losses")]
        public int Losses { get; set; }

        [JsonPropertyName("saves")]
        public int Saves { get; set; }

        [JsonPropertyName("holds")]
        public int Holds { get; set; }

        [JsonPropertyName("blownSaves")]
        public int BlownSaves { get; set; }

        [JsonPropertyName("gamesPitched")]
        public int GamesPitched { get; set; }

        [JsonPropertyName("pitchesThrown")]
        public int PitchesThrown { get; set; }

        [JsonPropertyName("balls")]
        public int Balls { get; set; }

        [JsonPropertyName("strikes")]
        public int Strikes { get; set; }

        [JsonPropertyName("strikePercentage")]
        public string StrikePercentage { get; set; }

        [JsonPropertyName("gamesFinished")]
        public int GamesFinished { get; set; }

        [JsonPropertyName("winPercentage")]
        public string WinPercentage { get; set; }

        [JsonPropertyName("pitchesPerInning")]
        public string PitchesPerInning { get; set; }

        [JsonPropertyName("strikeoutWalkRatio")]
        public string StrikeoutWalkRatio { get; set; }

        [JsonPropertyName("strikeoutsPer9Inn")]
        public string StrikeoutsPer9Inn { get; set; }

        [JsonPropertyName("walksPer9Inn")]
        public string WalksPer9Inn { get; set; }

        [JsonPropertyName("hitsPer9Inn")]
        public string HitsPer9Inn { get; set; }

        public decimal Score()
        {
            decimal completeGameShutoutPoints = 0;
            decimal noHitterPoints = 0;
            if(CompleteGames == 1 && EarnedRuns == 0)
            {
                completeGameShutoutPoints = 2.5M;
            }
            if(CompleteGames == 1 && Hits == 0)
            {
                noHitterPoints = 5;
            }
            return Outs * 0.75M +
                StrikeOuts * 2M +
                Wins * 4M +
                EarnedRuns * -2M +
                Hits * -0.6M +
                BaseOnBalls * -0.6M +
                HitBatsmen * -0.6M +
                CompleteGames * 2.5M +
                completeGameShutoutPoints +
                noHitterPoints;
        }
    }
}
