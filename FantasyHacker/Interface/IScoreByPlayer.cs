using System;
using System.Collections.Generic;
using FantasyHacker.BoxScoreResponse;
namespace FantasyHacker.Interface
{
    public interface IScoreByPlayer
    {
        public List<IFantasyPlayer> Score();
    }
}
