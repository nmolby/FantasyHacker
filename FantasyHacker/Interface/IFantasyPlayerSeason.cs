using System;
using System.Collections.Generic;
using FantasyHacker.BoxScoreResponse;
using FantasyHacker.PersonResponse;

namespace FantasyHacker.Interface
{
    public interface IFantasyPlayerSeason
    {
        public FantasyHacker.BoxScoreResponse.Person Person { get; set; }

        public List<Split> seasonGameStats { get; set; }
    }
}

