using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic.Match;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace F4DEDTournamentTests.Match
{
    [TestClass()]
    public class MatchManagerTests
    {
        [TestMethod()]
        public void CreateMatch_Successfull()
        {
            //Arrange
            MatchManager matchManager = new MatchManager("test");
            MatchDTO matchDTO = new MatchDTO()
            {
                ID = "1",
                Game = Games.CallOfDuty,
                PlayDate = DateTime.Now
            };

            //Act
            var result = matchManager.CreateMatch(matchDTO);

            //Assert
            Assert.AreEqual(MatchErrorCodes.NoError, result);
        }
    }
}