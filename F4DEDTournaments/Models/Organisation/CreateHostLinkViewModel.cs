using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace F4DEDTournaments.Models.Organisation
{
    public class CreateHostLinkViewModel
    {
        public string OrganisationID { get; set; }
        public List<OrganisationDTO> Organisations { get; set; }
    }
}
