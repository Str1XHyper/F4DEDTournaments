using DAL.Organisation;
using IdGenerator;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Organisation
{
    public enum OrganisationErrorCodes
    {
        UnknownException = -1,
        NoError = 0,
        NameAlreadyExists = 1,
    }
    public class OrganisationManager
    {
        OrganisationDB organisationDB = new OrganisationDB();
        Generator idGenerator = new Generator();

        public OrganisationDTO GetOrganisationByID(string ID) => organisationDB.FindOrganisationByID(ID);

        public OrganisationErrorCodes CreateOrganisation(OrganisationDTO organisationDTO)
        {
            var OrgName = organisationDB.FindOrganisationByName(organisationDTO.Name);
            if(OrgName != null)
            {
                return OrganisationErrorCodes.NameAlreadyExists;
            }
            organisationDTO.ID = idGenerator.GenerateID(12);
            var result = organisationDB.CreateOrganisation(organisationDTO);
            if (result == false)
            {
                return OrganisationErrorCodes.UnknownException;
            }
            return OrganisationErrorCodes.NoError;
        }
    }
}
