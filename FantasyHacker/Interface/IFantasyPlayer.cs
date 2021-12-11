using System;
using FantasyHacker.BoxScoreResponse;
namespace FantasyHacker.Interface
{
    /// <summary>
    /// An IFantasyPlayer is a player for a specific fantasy game.
    /// They have information about their Person, their Position, and Stats.
    /// </summary>
    public interface IFantasyPlayer : IFantasyScoreable
    {
        public Person Person { get; }

        public Position Position { get; }

        public Stats GameStats { get; }
    }
}
