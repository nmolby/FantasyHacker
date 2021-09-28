using System;
using System.Collections.Generic;

namespace FantasyHacker.Interface
{
    public interface ISeason
    {
        public List<IGame> Games { get; set; }
    }
}

