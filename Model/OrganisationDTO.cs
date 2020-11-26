using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class OrganisationDTO
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Countries CountryOfOrigin { get; set; }
    }
}
