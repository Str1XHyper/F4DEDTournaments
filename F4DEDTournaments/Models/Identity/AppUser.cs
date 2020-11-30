using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Model;

namespace F4DEDTournaments.Models
{
    public class AppUser : IdentityUser
    {
        public string Description { get; set; }

        [Required]
        public Countries CountryOfHeritage { get; set; }
        [Required]
        public Languages SpokenLanguages { get; set; }
        [Required]
        public DateTime Birthday { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public int Elo { get; set; }
    }
}
