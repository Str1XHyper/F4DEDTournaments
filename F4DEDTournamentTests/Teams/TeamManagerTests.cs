using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic.Teams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace F4DEDTournamentTests.Teams
{
    [TestClass()]
    public class TeamManagerTests
    {
        [TestMethod()]
        public void CreateTeam_Successfull()
        {
            //Arrange
            TeamManager teamManager = new TeamManager("test");
            TeamDTO DTO = new TeamDTO()
            {
                TeamID = "",
                TeamName = "TestName",
                Description = "TestDescription",
                MinimumAge = 18,
                MinimumElo = 1500,
                Country = Countries.Netherlands,
                IsPrivate = false,
                Language = Languages.Dutch,
                PlayedGame = Games.CallOfDuty
            };
            string UserID = "A1B2C3D4";

            //Act
            var result = teamManager.CreateTeam(DTO, UserID);

            //Assert
            Assert.AreEqual(TeamErrorCodes.NoError, result);
        }
        [TestMethod()]
        public void CreateTeam_NameAlreadyInUse()
        {
            //Arrange
            TeamManager teamManager = new TeamManager("test");
            TeamDTO DTO = new TeamDTO()
            {
                TeamID = "",
                TeamName = "A Used Name",
                Description = "TestDescription",
                MinimumAge = 18,
                MinimumElo = 1500,
                Country = Countries.Netherlands,
                IsPrivate = false,
                Language = Languages.Dutch,
                PlayedGame = Games.CallOfDuty
            };
            string UserID = "A1B2C3D4";

            //Act
            var result = teamManager.CreateTeam(DTO, UserID);

            //Assert
            Assert.AreEqual(TeamErrorCodes.NameAlreadyExists, result);
        }
        [TestMethod()]
        public void GetTeamByID_Successfull()
        {
            //Arrange
            TeamManager teamManager = new TeamManager("test");

            //Act
            var result = teamManager.GetTeamByID("1");

            //Assert
            Assert.AreEqual("1", result.TeamID);
        }
        [TestMethod()]
        public void GetTeamByID_StringEmpty()
        {
            //Arrange
            TeamManager teamManager = new TeamManager("test");

            //Act
            var result = teamManager.GetTeamByID("");

            //Assert
            Assert.IsNull(result);
        }

        [TestMethod()]
        public void GetTeamByID_InputNull()
        {
            //Arrange
            TeamManager teamManager = new TeamManager("test");

            //Act
            var result = teamManager.GetTeamByID(null);

            //Assert
            Assert.IsNull(result);
        }

        [TestMethod()]
        public void GetUserTeam_Successfull()
        {
            //Arrange
            TeamManager teamManager = new TeamManager("test");
            string UserID = "1";

            //Act
            var result = teamManager.GetUserTeam(UserID);

            //Assert
            Assert.AreEqual(TeamRoles.Admin, result.Role);
        }

        [TestMethod()]
        public void GetUserTeam_StringEmpty()
        {
            //Arrange
            TeamManager teamManager = new TeamManager("test");
            string UserID = "";

            //Act
            var result = teamManager.GetUserTeam(UserID);

            //Assert
            Assert.IsNull(result);
        }
        [TestMethod()]
        public void GetUserTeam_InputNull()
        {
            //Arrange
            TeamManager teamManager = new TeamManager("test");
            string UserID = null;

            //Act
            var result = teamManager.GetUserTeam(UserID);

            //Assert
            Assert.IsNull(result);
        }

        [TestMethod()]
        public void GetTeamByUser_Successfull()
        {
            //Arrange
            TeamManager teamManager = new TeamManager("test");
            string UserID = "1";

            //Act
            var result = teamManager.GetTeamByUser(UserID);

            //Assert
            Assert.AreEqual("FindTeamByUser Team", result.TeamName);
        }
        [TestMethod()]
        public void GetTeamByUser_StringEmpty()
        {
            //Arrange
            TeamManager teamManager = new TeamManager("test");
            string UserID = "";

            //Act
            var result = teamManager.GetTeamByUser(UserID);

            //Assert
            Assert.IsNull(result);
        }
        [TestMethod()]
        public void GetTeamByUser_InputNull()
        {
            //Arrange
            TeamManager teamManager = new TeamManager("test");
            string UserID = null;

            //Act
            var result = teamManager.GetTeamByUser(UserID);

            //Assert
            Assert.IsNull(result);
        }

        [TestMethod()]
        public void GetTop10Teams_Successfull()
        {
            //Arrange
            TeamManager teamManager = new TeamManager("test");

            //Act
            var result = teamManager.GetTop10Teams();

            //Assert
            Assert.IsTrue(result.Count <= 10);
        }
    }
}