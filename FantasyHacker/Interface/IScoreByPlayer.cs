using System;
using System.Collections.Generic;
using FantasyHacker.BoxScore;
namespace FantasyHacker.Interface
{
    public interface IScoreByPlayer
    {
        public List<IFantasyPlayer> Score();
    }
}
