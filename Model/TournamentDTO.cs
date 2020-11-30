using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class TournamentDTO
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string OrganisationID { get; set; }
        public int Size { get; set; }
        public int Prize { get; set; }
        public int BuyIn { get; set; }
        public Games Game { get; set; }
        public DateTime StartTime { get; set; }
    }
}
