using System;
using System.Text.Json.Serialization;
using FantasyHacker.Interface;
using System.Collections.Generic;

namespace FantasyHacker.BoxScore
{
    public class Player : IFantasyPlayer
    {
        [JsonPropertyName("person")]
        public Person Person { get; set; }

        [JsonPropertyName("jerseyNumber")]
        public string JerseyNumber { get; set; }

        [JsonPropertyName("position")]
        public Position Position { get; set; }

        [JsonPropertyName("stats")]
        public Stats Stats { get; set; }

        [JsonPropertyName("status")]
        public Status Status { get; set; }

        [JsonPropertyName("parentTeamId")]
        public int ParentTeamId { get; set; }

        [JsonPropertyName("battingOrder")]
        public string BattingOrder { get; set; }

        [JsonPropertyName("seasonStats")]
        public SeasonStats SeasonStats { get; set; }

        [JsonPropertyName("gameStatus")]
        public GameStatus GameStatus { get; set; }

        [JsonPropertyName("allPositions")]
        public List<AllPosition> AllPositions { get; set; }

        public decimal Score()
        {
            if(Position.Code == "1")
            {
                return Stats.Pitching.Score();
            } else
            {
                return Stats.Batting.Score();
            }
        }
    }
}
