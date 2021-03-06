﻿using DAL.Organisation;
using DalFactory;
using IdGenerator;
using Interface.Organisation;
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
        IOrganisationManagerDB organisationDB = OrganisationFactory.GetOrganisationManagerDB("release");
        Generator idGenerator = new Generator();

        public OrganisationManager(string source)
        {
            organisationDB = OrganisationFactory.GetOrganisationManagerDB(source);
        }
        public OrganisationManager()
        {

        }

        public OrganisationDTO GetOrganisationByID(string ID)
        {
            if(ID == null || ID == string.Empty)
            {
                return null;
            }

            return organisationDB.FindOrganisationByID(ID);
        }

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

        public List<OrganisationDTO> GetAllOrganisations() => organisationDB.getAllOrganisations();

        public string CreateHostLink(string organisationID)
        {
            return $"https://f4dedtournaments-dev-as.azurewebsites.net/Tournament/CreateTournament?OrganisationID={organisationID}";
        }
    }
}
