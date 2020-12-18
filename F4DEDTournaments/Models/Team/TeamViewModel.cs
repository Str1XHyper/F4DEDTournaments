using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace F4DEDTournaments.Models
{
    public class TeamViewModel
    {
        public Logic.Teams.Team Team { get; set; }
        public TeamRoles Role { get; set; }
        public List<string> Members { get; set; }
        public List<TeamMatchResultDTO> RecentMatches { get; set; }
    }
}
