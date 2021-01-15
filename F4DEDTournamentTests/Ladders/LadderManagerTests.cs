using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic.Ladders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace F4DEDTournamentTests.Ladder
{
    [TestClass()]
    public class LadderManagerTests
    {
        [TestMethod()]
        public void CreateLadder_Successfull()
        {
            //Arrange
            LadderManager ladderManager = new LadderManager("test");
            LadderDTO ladderDTO = new LadderDTO()
            {
                ID = "",
                Game = Games.CallOfDuty,
                MaximumElo = 1500,
                MinimumElo = 1000,
                Name = "Bronze Call of Duty Ladder"
            };

            //Act
            var result = ladderManager.CreateLadder(ladderDTO);

            //Assert
            Assert.AreEqual(LadderErrorCodes.NoError, result);
        }
        [TestMethod()]
        public void CreateLadder_MinHigherThanMax()
        {
            //Arrange
            LadderManager ladderManager = new LadderManager("test");
            LadderDTO ladderDTO = new LadderDTO()
            {
                ID = "",
                Game = Games.CallOfDuty,
                MaximumElo = 1500,
                MinimumElo = 2000,
                Name = "Bronze Call of Duty Ladder"
            };

            //Act
            var result = ladderManager.CreateLadder(ladderDTO);

            //Assert
            Assert.AreEqual(LadderErrorCodes.MinHigherThanMax, result);
        }
    }
}