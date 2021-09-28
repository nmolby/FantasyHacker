using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using FantasyHacker.Interface;
namespace FantasyHacker.BoxScoreResponse
{
    public class BoxScore : IGame
    {
        [JsonPropertyName("copyright")]
        public string Copyright { get; set; }

        [JsonPropertyName("teams")]
        public Teams Teams { get; set; }

        [JsonPropertyName("officials")]
        public List<Official> Officials { get; set; }

        [JsonPropertyName("info")]
        public List<Info> Info { get; set; }

        [JsonPropertyName("pitchingNotes")]
        public List<object> PitchingNotes { get; set; }

        public List<IFantasyPlayer> Score()
        {
            var playerScores = new List<IFantasyPlayer>();
            var teams = new List<ITeamGame> { Teams.Away, Teams.Home };
            foreach (ITeamGame teamGame in teams)
            {
                playerScores.AddRange(teamGame.Players.Values);
            }

            return playerScores;
        }
    }
}
