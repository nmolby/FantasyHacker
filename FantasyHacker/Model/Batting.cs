using System.Collections.Generic;
using System.Text.Json.Serialization;
using FantasyHacker.Interface;

namespace FantasyHacker.BoxScore
{
    public class Batting : IFantasyScoreable
    {
        [JsonPropertyName("flyOuts")]
        public int FlyOuts { get; set; }

        [JsonPropertyName("groundOuts")]
        public int GroundOuts { get; set; }

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

        [JsonPropertyName("avg")]
        public string Avg { get; set; }

        [JsonPropertyName("atBats")]
        public int AtBats { get; set; }

        [JsonPropertyName("obp")]
        public string Obp { get; set; }

        [JsonPropertyName("slg")]
        public string Slg { get; set; }

        [JsonPropertyName("ops")]
        public string Ops { get; set; }

        [JsonPropertyName("caughtStealing")]
        public int CaughtStealing { get; set; }

        [JsonPropertyName("stolenBases")]
        public int StolenBases { get; set; }

        [JsonPropertyName("stolenBasePercentage")]
        public string StolenBasePercentage { get; set; }

        [JsonPropertyName("groundIntoDoublePlay")]
        public int GroundIntoDoublePlay { get; set; }

        [JsonPropertyName("groundIntoTriplePlay")]
        public int GroundIntoTriplePlay { get; set; }

        [JsonPropertyName("plateAppearances")]
        public int PlateAppearances { get; set; }

        [JsonPropertyName("totalBases")]
        public int TotalBases { get; set; }

        [JsonPropertyName("rbi")]
        public int Rbi { get; set; }

        [JsonPropertyName("leftOnBase")]
        public int LeftOnBase { get; set; }

        [JsonPropertyName("sacBunts")]
        public int SacBunts { get; set; }

        [JsonPropertyName("sacFlies")]
        public int SacFlies { get; set; }

        [JsonPropertyName("catchersInterference")]
        public int CatchersInterference { get; set; }

        [JsonPropertyName("pickoffs")]
        public int Pickoffs { get; set; }

        [JsonPropertyName("atBatsPerHomeRun")]
        public string AtBatsPerHomeRun { get; set; }

        [JsonPropertyName("gamesPlayed")]
        public int GamesPlayed { get; set; }

        [JsonPropertyName("babip")]
        public string Babip { get; set; }

        [JsonPropertyName("note")]
        public string Note { get; set; }

        public decimal Score()
        {
            int singles = Hits - Doubles - Triples - HomeRuns;
            decimal score = singles * 3 +
                Doubles * 5 +
                Triples * 8 +
                HomeRuns * 10 +
                Rbi * 2 +
                Runs * 2 +
                BaseOnBalls * 2 +
                HitByPitch * 2 +
                StolenBases * 5;

            return score;
        }
    }
}
