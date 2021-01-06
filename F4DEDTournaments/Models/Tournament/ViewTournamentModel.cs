using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace F4DEDTournaments.Models.Tournament
{
    public class ViewTournamentModel
    {
        public Logic.Tournament.Tournament Tournament { get; set; }
        public bool IsOwner { get; set; }
        public string[] CurrentUsers { get; set; }
    }
}
