using System;
using System.Collections.Generic;

namespace FantasyHacker.Interface
{
    /// <summary>
    /// An algorithm that takes a list of Fantasy Players and gives each of them a rank
    /// </summary>
    /// <typeparam name="IFantasyPlayer">Any type of Fantasy Player</typeparam>
    public interface IAlgorithm<IFantasyPlayer>
    {
        /// <summary>
        /// Ranks the list of fantasy players
        /// </summary>
        /// <param name="players">A list of fantasy players</param>
        /// <returns></returns>
        public Dictionary<IFantasyPlayer, decimal> RankPlayers(IEnumerable<IFantasyPlayer> players);
    }
}

