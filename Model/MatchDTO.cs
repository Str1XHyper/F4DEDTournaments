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
        public DateTime PlayedDate { get; set; }
        public int TeamWon { get; set; }
    }
}
