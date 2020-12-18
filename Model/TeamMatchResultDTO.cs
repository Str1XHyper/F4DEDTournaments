using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class TeamMatchResultDTO
    {
        public bool Won { get; set; }
        public int ScoreSelf { get; set; }
        public int ScoreOpponent { get; set; }
        public string OpponentName { get; set; }
    }
}
