using System;
using System.Collections.Generic;
using FantasyHacker.BoxScoreResponse;

namespace FantasyHacker.Interface
{
    public interface ITeamGame
    {
        public Team Team { get; set; }

        public TeamStats TeamStats { get; set; }

        public Dictionary<string, Player> Players { get; set; }

        public List<int> Batters { get; set; }

        public List<int> Pitchers { get; set; }

        public List<int> Bench { get; set; }

        public List<int> Bullpen { get; set; }

        public List<int> BattingOrder { get; set; }

        public List<Info> Info { get; set; }

        public List<Note> Note { get; set; }
    }
}
