using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace F4DEDTournaments.Models.Organisation
{
    public class CreateOrganisationViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public Countries CountryOfOrigin { get; set; }
    }
}
