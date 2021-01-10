using Model;
using System.Collections.Generic;

namespace Interface.Organisation
{
    public interface IOrganisationManagerDB
    {
        bool CreateOrganisation(OrganisationDTO organisationDTO);
        OrganisationDTO FindOrganisationByID(string ID);
        OrganisationDTO FindOrganisationByName(string Name);
        List<OrganisationDTO> getAllOrganisations();
    }
}