using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace Logic.Teams
{
    public class Team
    {
        public string TeamID { get; set; }
        public string TeamName { get; set; }
        public int MinimumElo { get; set; }
        public bool IsPrivate { get; set; }
        public string Description { get; set; }
        public Countries Country { get; set; }
        public Languages Language { get; set; }
        public int MinimumAge { get; set; }
        public Games PlayedGame { get; set; }

        public Team(TeamDTO teamDTO)
        {
            TeamID = teamDTO.TeamID;
            TeamName = teamDTO.TeamName;
            MinimumAge = teamDTO.MinimumAge;
            MinimumElo = teamDTO.MinimumElo;
            IsPrivate = teamDTO.IsPrivate;
            Description = teamDTO.Description;
            Country = teamDTO.Country;
            Language = teamDTO.Language;
            PlayedGame = teamDTO.PlayedGame;
        }
    }
}
