using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace F4DEDTournaments.Models.Account
{
    public class ProfileViewModel
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public Countries CountryOfHeritage { get; set; }
        public Languages MainLanguage { get; set; }
        public bool IsLoggedInUser { get; set; }
    }
}
