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
    public class TeamTests
    {
        TeamDTO testTeamDTO = new TeamDTO()
        {
            IsPrivate = false,
            TeamID = "1234asdf",
            Country = Countries.Belgium,
            MinimumAge = 16,
            TeamName = "FindTeamByUser Team",
            Description = "Een FindTeamByUser test team",
            Language = Languages.Dutch,
            MinimumElo = 1000,
            PlayedGame = Games.CallOfDuty
        };

        [TestMethod()]
        public void GetResults_Successfull()
        {
            //Arrange
            Team testTeam = new Team(testTeamDTO, "test");

            //Act
            var results = testTeam.GetResults();

            //Assert
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("Danillo", results[0].OpponentName);
        }

        [TestMethod()]
        public void Update_Successfull()
        {
            //Arrange
            Team testTeam = new Team(testTeamDTO, "test");

            //Act
            var result = testTeam.UpdateTeam();

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void AddPlayer_Successfull()
        {
            //Arrange
            Team testTeam = new Team(testTeamDTO, "test");
            string UserID = "1";
            TeamRoles role = TeamRoles.Member;

            //Act
            var result = testTeam.AddPlayer(UserID, role);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void AddPlayer_NoUserID()
        {
            //Arrange
            Team testTeam = new Team(testTeamDTO, "test");
            string UserID = "";
            TeamRoles role = TeamRoles.Member;

            //Act
            var result = testTeam.AddPlayer(UserID, role);

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod()]
        public void AddPlayer_UserIDNull()
        {
            //Arrange
            Team testTeam = new Team(testTeamDTO, "test");
            string UserID = null;
            TeamRoles role = TeamRoles.Member;

            //Act
            var result = testTeam.AddPlayer(UserID, role);

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod()]
        public void AddPlayer_NonMemberRole()
        {
            //Arrange
            Team testTeam = new Team(testTeamDTO, "test");
            string UserID = "1";
            TeamRoles role = TeamRoles.NonMember;

            //Act
            var result = testTeam.AddPlayer(UserID, role);

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod()]
        public void GetMembers_Successfull()
        {
            //Arrange
            Team testTeam = new Team(testTeamDTO, "test");
            List<string> correctList = new List<string>()
            {
                "Tijn",
                "Danillo",
                "Luuk"
            };

            //Act
            var result = testTeam.GetMembers();

            //Assert
            Assert.IsTrue(result.Count == 3) ;
            CollectionAssert.AreEquivalent(correctList, result);
        }
    }
}