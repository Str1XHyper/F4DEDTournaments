using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class MatchDTO
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public Games Game { get; set; }
        public DateTime PlayDate { get; set; }
        public int ScoreTeam1 { get; set; }
        public int ScoreTeam2 { get; set; }
    }
}
