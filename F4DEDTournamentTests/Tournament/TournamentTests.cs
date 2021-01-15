using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic.Tournament;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace F4DEDTournamentTests.Tournaments
{
    [TestClass()]
    public class TournamentTests
    {
        TournamentDTO testDTO = new TournamentDTO()
        {
            ID = "A1B2C3D4",
            Name = "Test Tournament",
            OrganisationID = "null",
            UserID = "021d968c-b151-4399-903e-19d867f08e5a",
            Size = 32,
            Prize = 0,
            BuyIn = 0,
            Game = Games.CounterStrike,
            StartTime = new DateTime(2021, 2, 1, 12, 00, 00),
            Status = TourneyStatus.Active,
            TeamSize = 1
        };

        [TestMethod()]
        public void GetUsers_Successfull()
        {
            //Arrange
            Tournament testTournament = new Tournament(testDTO, "test");
            string[] correctList = new string[]
            {
                "Tijn",
                "Danillo",
                "Luuk"
            };

            //Act
            var result = testTournament.GetUsers();

            //Assert
            Assert.IsTrue(result.Length == 3);
            CollectionAssert.AreEquivalent(correctList, result);
        }

        [TestMethod()]
        public void AddUser_Successfull()
        {
            //Arrange
            Tournament testTournament = new Tournament(testDTO, "test");
            string UserID = "1";

            //Act
            var result = testTournament.AddUser(UserID);

            //Assert
            Assert.AreEqual(TournamentErrorCode.NoError, result);
        }

        [TestMethod()]
        public void AddUser_StringEmpty()
        {
            //Arrange
            Tournament testTournament = new Tournament(testDTO, "test");
            string UserID = "";

            //Act
            var result = testTournament.AddUser(UserID);

            //Assert
            Assert.AreEqual(TournamentErrorCode.CouldntAddUserToTournament, result);
        }
        [TestMethod()]
        public void AddUser_InputNull()
        {
            //Arrange
            Tournament testTournament = new Tournament(testDTO, "test");
            string UserID = "";

            //Act
            var result = testTournament.AddUser(UserID);

            //Assert
            Assert.AreEqual(TournamentErrorCode.CouldntAddUserToTournament, result);
        }

        [TestMethod()]
        public void AddTeam_Successfull()
        {
            //Arrange
            Tournament testTournament = new Tournament(testDTO, "test");
            string TeamID = "1";

            //Act
            var result = testTournament.AddTeam(TeamID);

            //Assert
            Assert.AreEqual(TournamentErrorCode.NoError, result);
        }

        [TestMethod()]
        public void AddTeam_StringEmpty()
        {
            //Arrange
            Tournament testTournament = new Tournament(testDTO, "test");
            string TeamID = "";

            //Act
            var result = testTournament.AddTeam(TeamID);

            //Assert
            Assert.AreEqual(TournamentErrorCode.CouldntAddTeamToTournament, result);
        }

        [TestMethod()]
        public void AddTeam_InputNull()
        {
            //Arrange
            Tournament testTournament = new Tournament(testDTO, "test");
            string TeamID = null;

            //Act
            var result = testTournament.AddTeam(TeamID);

            //Assert
            Assert.AreEqual(TournamentErrorCode.CouldntAddTeamToTournament, result);
        }


        [TestMethod()]
        public void Start_Successfull()
        {
            //Arrange
            Tournament testTournament = new Tournament(testDTO, "test");

            //Act
            var result = testTournament.Start();

            //Assert
            Assert.AreEqual(TournamentErrorCode.NoError, result);
            Assert.AreEqual(TourneyStatus.Active, testTournament.Status);
        }

        [TestMethod()]
        public void End_Successfull()
        {
            //Arrange
            Tournament testTournament = new Tournament(testDTO, "test");

            //Act
            var result = testTournament.End();

            //Assert
            Assert.AreEqual(TournamentErrorCode.NoError, result);
            Assert.AreEqual(TourneyStatus.Finished, testTournament.Status);
        }
    }
}