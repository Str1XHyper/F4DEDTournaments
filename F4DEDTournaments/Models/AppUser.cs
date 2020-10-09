using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace F4DEDTournaments.Models
{
    public enum Countries
    {
        UnitedKingdom = 1,
        UnitedStates = 2,
        Canada = 3,
        Netherlands = 4,
        Belgium = 5,
        France = 6,
        Germany = 7,
        Japan = 8,
        China = 9
    }

    public enum Languages
    {
        English = 1,
        Dutch = 2,
        French = 3,
        German = 4,
        Japanese = 5,
        Chinees = 6
    }
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
    }
}
