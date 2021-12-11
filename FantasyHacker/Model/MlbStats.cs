using System;
using System.Collections.Generic;

namespace FantasyHacker.Model
{
    public struct MlbStats
    {
        public string BA { get; set; }
        public string OBP { get; set; }
        public string SLG { get; set; }
        public string BAPIP { get; set; }
        public string SBPercentage { get; set; }
        public int SBCount { get; set; }
        public int HitCount { get; set; }
        public int PACount { get; set; }
        public int TotalBaseCount { get; set; }
        public int RunCount { get; set; }
        public int RBICount { get; set; }
        public int HomeRunCount { get; set; }
        public int WalkCount { get; set; }
        public int HBPCount { get; set; }
        public int AtBatCount { get; set; }
        public int SacFlyCount { get; set; }
        public int DoubleCount { get; set; }
        public int TripleCount { get; set; }
        public int SOCount { get; set; }
        public int CaughtStealingCount { get; set; }


        public SortedDictionary<string, int> labelCounts
        {
            get
            {
                return new SortedDictionary<string, int>
                {
                    { "Stolen Base Count" , SBCount },
                    { "Hit Count" , HitCount },
                    { "Plate Appearance Count" , PACount },
                    { "Total Base Count" , TotalBaseCount },
                    { "Run Count" , RunCount },
                    { "RBI Count" , RBICount },
                    { "Home Run Count" , HomeRunCount },
                    { "Walk Count" , WalkCount },
                    { "Hit By Pitch Count" , HBPCount },
                    { "At Bat Count" , AtBatCount },
                    { "Sac Fly Count" , SacFlyCount },
                    { "Double Count" , DoubleCount },
                    { "Triple Count" , TripleCount },
                    { "Strikeout Count" , SOCount },
                    { "Caught Stealing Count" , CaughtStealingCount }
                };

            }
        }


        public SortedDictionary<string, string> labelCalcullations
        {
            get
            {
                return new SortedDictionary<string, string>
                {
                    { "Batting Average" , BA },
                    { "On Base Percentage" , OBP },
                    { "Slugging Percentage" , SLG },
                    { "BAPIP" , BAPIP },
                    { "Stolen Base Percentage", SBPercentage }
                };

            }
        }
    }
}

