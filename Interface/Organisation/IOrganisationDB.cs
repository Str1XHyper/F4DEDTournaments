using Model;

namespace Interface.Organisation
{
    public interface IOrganisationDB
    {
        bool EditOrganisation(OrganisationDTO organisationDTO);
    }
}