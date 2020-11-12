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
        public string TeamID { get; set; }
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
