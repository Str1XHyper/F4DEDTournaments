using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace F4DEDTournaments.Models.Ladder
{
    public class CreateLadderViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public Games Game { get; set; }
        [Required]
        public int MinimumElo { get; set; }
        [Required]
        public int MaximumElo { get; set; }
    }
}
