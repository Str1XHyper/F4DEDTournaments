using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace F4DEDTournaments.Models.Team
{
    public class IndexViewModel
    {
        public List<Logic.Teams.Team> Teams { get; set; }

        public Logic.Teams.Team CurrentTeam { get; set; }
        public List<TeamMatchResultDTO> PreviousResults { get; set; }
        public List<string> CurrentRoster { get; set; }
    }
}
