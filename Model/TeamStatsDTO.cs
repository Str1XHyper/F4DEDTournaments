using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public struct TeamStatsDTO
    {
        public string TeamID { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
    }
}
