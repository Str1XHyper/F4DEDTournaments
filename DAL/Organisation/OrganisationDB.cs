using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Organisation
{
    public class OrganisationDB
    {
        public bool CreateOrganisation(OrganisationDTO organisationDTO)
        {
            List<string[]> param = new List<string[]>()
            {
                new string[] {"@ID", organisationDTO.ID},
                new string[] {"@Name", organisationDTO.Name},
                new string[] {"@Description", organisationDTO.Description},
                new string[] {"@Origin", ((int)organisationDTO.CountryOfOrigin).ToString()}
            };
            var result = SQLConnection.ExecuteNonSearchQueryParameters("INSERT INTO Organisation (ID,Name,Description,CountryOfOrigin) VALUES (@ID,@Name,@Description,@Origin)", param);
            return result;
        }

        public bool EditOrganisation(OrganisationDTO organisationDTO)
        {
            List<string[]> param = new List<string[]>()
            {
                new string[] {"@ID", organisationDTO.ID},
                new string[] {"@Name", organisationDTO.Name},
                new string[] {"@Description", organisationDTO.Description},
                new string[] {"@Origin", ((int)organisationDTO.CountryOfOrigin).ToString()}
            };
            var result = SQLConnection.ExecuteNonSearchQueryParameters("UPDATE Organisation SET Name = @Name, Description = @Description, CountryOfOrigin = @Origin WHERE ID = @ID", param);
            return result;
        }
        public OrganisationDTO FindOrganisationByID(string ID)
        {
            List<string[]> param = new List<string[]>()
            {
                new string[] {"@ID", ID}
            };
            var result = SQLConnection.ExecuteSearchQueryParameters("SELECT * FROM Organisation WHERE ID = @ID", param);
            return GenerateDTOFromRow(result[0]);
        }
        public OrganisationDTO FindOrganisationByName(string Name)
        {
            List<string[]> param = new List<string[]>()
            {
                new string[] {"@Name", Name}
            };
            var result = SQLConnection.ExecuteSearchQueryParameters("SELECT * FROM Organisation WHERE Name = @Name", param);
            if (result.Count == 0)
            {
                return null;
            }
            return GenerateDTOFromRow(result[0]);
        }
        public List<OrganisationDTO> getAllOrganisations()
        {
            var result = SQLConnection.ExecuteSearchQuery("SELECT * FROM Organisation");
            return GenerateDTOsFromRows(result);
        }
        private OrganisationDTO GenerateDTOFromRow(string[] row)
        {

            OrganisationDTO organisationDTO = new OrganisationDTO()
            {
                ID = row[0],
                Name = row[1],
                Description = row[2],
                CountryOfOrigin = (Countries) (Convert.ToInt32(row[3]))
            };
            return organisationDTO;
        }

        private List<OrganisationDTO> GenerateDTOsFromRows(List<string[]> rows)
        {
            List<OrganisationDTO> organisationList = new List<OrganisationDTO>();
            foreach (string[] row in rows)
            {
                organisationList.Add(new OrganisationDTO()
                {
                    ID = row[0],
                    Name = row[1],
                    Description = row[2],
                    CountryOfOrigin = (Countries)(Convert.ToInt32(row[3]))
                });
            }
            return organisationList;
        }
    }
}
