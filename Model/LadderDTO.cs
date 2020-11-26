using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class LadderDTO
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public Games Game { get; set; }
        public int MinimumElo { get; set; }
        public int MaximumElo { get; set; }
    }
}
