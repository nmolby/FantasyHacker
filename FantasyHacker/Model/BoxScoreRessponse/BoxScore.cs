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

        public List<IFantasyPlayer> GetPlayers()
        {
            var players = new List<IFantasyPlayer>();
            var teams = new List<ITeamGame> { Teams.Away, Teams.Home };
            foreach (ITeamGame teamGame in teams)
            {
                players.AddRange(teamGame.Players.Values);
            }

            return players;
        }
    }
}
