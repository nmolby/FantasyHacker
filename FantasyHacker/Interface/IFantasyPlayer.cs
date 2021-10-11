using System;
using FantasyHacker.BoxScoreResponse;
namespace FantasyHacker.Interface
{
    public interface IFantasyPlayer : IFantasyScoreable
    {
        public Person Person { get; }

        public Position Position { get; }

        public Stats Stats { get; }
    }
}
