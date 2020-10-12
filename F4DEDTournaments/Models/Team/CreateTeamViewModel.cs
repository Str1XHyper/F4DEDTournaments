using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace F4DEDTournaments.Models
{
    public enum Games
    {
        CounterStrike = 0,
        CallOfDuty = 1,
        RainbowSixSiege = 2,
        Valorant = 3,
    }

    public class CreateTeamViewModel
    {
        [Required]
        public string TeamName { get; set; }
        [Required]
        public int MinimumElo { get; set; }
        [Required]
        public bool IsPrivate { get; set; }
        public string Description { get; set; }
        [Required]
        public Countries Country { get; set; }
        [Required]
        public Languages Language { get; set; }
        [Required]
        public int MinimumAge { get; set; }
        [Required]
        public Games PlayedGame { get; set; }
    }
}
