using Interface.Organisation;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestDAL.Organisation
{
    public class TestOrganisationDB : IOrganisationManagerDB
    {
        public bool CreateOrganisation(OrganisationDTO organisationDTO)
        {
            return true;
        }

        public OrganisationDTO FindOrganisationByID(string ID)
        {
            OrganisationDTO testDTO = new OrganisationDTO()
            {
                ID = ID,
                CountryOfOrigin = Countries.Netherlands,
                Description = "Test Description",
                Name = "Test Name",
            };
            return testDTO;
        }

        public OrganisationDTO FindOrganisationByName(string Name)
        {
            if(Name == "A used name")
            {
                OrganisationDTO testDTO = new OrganisationDTO()
                {
                    ID = "1",
                    CountryOfOrigin = Countries.Netherlands,
                    Description = "Test Description",
                    Name = "Test Name",
                };
                return testDTO;
            }
            return null;
        }

        public List<OrganisationDTO> getAllOrganisations()
        {
            return new List<OrganisationDTO>()
            {
                new OrganisationDTO()
                {
                    ID = "1",
                    CountryOfOrigin = Countries.Netherlands,
                    Description = "Test Description",
                    Name = "Test Name",
                },
                new OrganisationDTO()
                {
                    ID = "2",
                    CountryOfOrigin = Countries.Belgium,
                    Description = "Test Description",
                    Name = "SecondOrganisation",
                },
            };

        }
    }
}
