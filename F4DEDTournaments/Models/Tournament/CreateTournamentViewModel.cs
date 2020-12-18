using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Model;

namespace F4DEDTournaments.Models.Tournament
{
    public class CreateTournamentViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string OrganisationID { get; set; }
        [Required]
        public int Size { get; set; }
        [Required]
        public int Prize { get; set; }
        [Required]
        public int BuyIn { get; set; }
        [Required]
        public Games Game { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm}", ApplyFormatInEditMode = true)]
        public DateTime StartTime { get; set; }
    }
}
