using System;
using System.Collections.Generic;
using FantasyHacker.BoxScoreResponse;
namespace FantasyHacker.Interface
{
    /// <summary>
    /// A fantasy game that has a list of IFantasyPlayers.
    /// </summary>
    public interface IGame
    {
        public List<IFantasyPlayer> GetPlayers();
    }
}
