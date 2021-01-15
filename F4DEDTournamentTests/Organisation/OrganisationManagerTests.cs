using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic.Organisation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace F4DEDTournamentTests.Organisation
{
    [TestClass()]
    public class OrganisationManagerTests
    {
        [TestMethod()]
        public void GetOrganisationByID_Successfull()
        {
            //Arrange
            OrganisationManager organisationManager = new OrganisationManager("test");
            string OrgID = "1";

            //Act
            var result = organisationManager.GetOrganisationByID(OrgID);

            //Assert
            Assert.AreEqual("1", result.ID);
        }
        [TestMethod()]
        public void GetOrganisationByID_StringEmpty()
        {
            //Arrange
            OrganisationManager organisationManager = new OrganisationManager("test");
            string OrgID = "";

            //Act
            var result = organisationManager.GetOrganisationByID(OrgID);

            //Assert
            Assert.IsNull(result);
        }
        [TestMethod()]
        public void GetOrganisationByID_IDisNull()
        {
            //Arrange
            OrganisationManager organisationManager = new OrganisationManager("test");
            string OrgID = null;

            //Act
            var result = organisationManager.GetOrganisationByID(OrgID);

            //Assert
            Assert.IsNull(result);
        }

        [TestMethod()]
        public void CreateOrganisation_Successfull()
        {
            //Arrange
            OrganisationManager organisationManager = new OrganisationManager("test");
            OrganisationDTO organisationDTO = new OrganisationDTO()
            {
                ID = "",
                CountryOfOrigin = Countries.France,
                Description = "Test Description",
                Name = "Test Name",
            };

            //Act
            var result = organisationManager.CreateOrganisation(organisationDTO);

            //Assert
            Assert.AreEqual(OrganisationErrorCodes.NoError, result);

        }

        [TestMethod()]
        public void CreateOrganisation_NameUsed()
        {
            //Arrange
            OrganisationManager organisationManager = new OrganisationManager("test");
            OrganisationDTO organisationDTO = new OrganisationDTO()
            {
                ID = "",
                CountryOfOrigin = Countries.France,
                Description = "Test Description",
                Name = "A used name",
            };

            //Act
            var result = organisationManager.CreateOrganisation(organisationDTO);

            //Assert
            Assert.AreEqual(OrganisationErrorCodes.NameAlreadyExists, result);

        }

        [TestMethod()]
        public void GetAllOrganisations_Successfull()
        {
            //Arrange
            OrganisationManager organisationManager = new OrganisationManager("test");

            //Act
            var result = organisationManager.GetAllOrganisations();

            //Assert
            Assert.AreEqual(2, result.Count);
        }

        [TestMethod()]
        public void CreateHostLink_Successfull()
        {
            //Arrange
            OrganisationManager organisationManager = new OrganisationManager("test");
            string OrgID = "1";
            string CorrectString = $"https://f4dedtournaments-dev-as.azurewebsites.net/Tournament/CreateTournament?OrganisationID={OrgID}";


            //Act
            var result = organisationManager.CreateHostLink(OrgID);

            //Assert
            Assert.AreEqual(CorrectString, result);
        }
    }
}