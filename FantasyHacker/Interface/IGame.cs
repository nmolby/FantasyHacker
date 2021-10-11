using System;
using System.Collections.Generic;
using FantasyHacker.BoxScoreResponse;
namespace FantasyHacker.Interface
{
    public interface IGame
    {
        public List<IFantasyPlayer> Score();
    }
}
