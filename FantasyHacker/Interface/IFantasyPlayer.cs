using System;
using FantasyHacker.BoxScoreResponse;
namespace FantasyHacker.Interface
{
    public interface IFantasyPlayer : IFantasyScoreable
    {
        public Person Person { get; set; }

        public Position Position { get; set; }

        public Stats Stats { get; set; }
    }
}
