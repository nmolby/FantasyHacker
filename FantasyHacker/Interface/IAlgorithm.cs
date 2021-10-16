using System;
using System.Collections.Generic;

namespace FantasyHacker.Interface
{
    public interface IAlgorithm<IFantasyPlayer>
    {
        public Dictionary<IFantasyPlayer, decimal> RankPlayers(List<IFantasyPlayer> players);
    }
}

