using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic.Tournament;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace F4DEDTournamentTests.Tournament
{
    [TestClass()]
    public class TournamentManagerTests
    {
        [TestMethod()]
        public void CreateTournament_CorrectInput_Test()
        {
            //Arrange
            TournamentManager tournament = new TournamentManager("Test");
            TournamentDTO tournamentDTO = new TournamentDTO()
            {
                ID = "",
                Name = "Test Tournament",
                OrganisationID = "null",
                UserID = "021d968c-b151-4399-903e-19d867f08e5a",
                Size = 32,
                Prize = 0,
                BuyIn = 0,
                Game = Games.CounterStrike,
                StartTime = new DateTime(2021, 2, 1, 12, 00, 00),
                Status = TourneyStatus.Planned,
                TeamSize = 1
            };

            //Act
            var result = tournament.CreateTournament(tournamentDTO);

            //Assert
            Assert.AreEqual(TournamentManagerErrorCodes.NoError, result);
        }
        [TestMethod()]
        public void CreateTournament_NoHost_Test()
        {
            //Arrange
            TournamentManager tournament = new TournamentManager("Test");
            TournamentDTO tournamentDTO = new TournamentDTO()
            {
                ID = "",
                Name = "Test Tournament",
                OrganisationID = "null",
                UserID = "null",
                Size = 32,
                Prize = 0,
                BuyIn = 0,
                Game = Games.CounterStrike,
                StartTime = new DateTime(2021, 2, 1, 12, 00, 00),
                Status = TourneyStatus.Planned,
                TeamSize = 1
            };

            //Act
            var result = tournament.CreateTournament(tournamentDTO);

            //Assert
            Assert.AreEqual(TournamentManagerErrorCodes.NoHost, result);
        }
        [TestMethod()]
        public void CreateTournament_BuyInMoreOrEqualToPrize_Test()
        {
            //Arrange
            TournamentManager tournament = new TournamentManager("Test");
            TournamentDTO tournamentDTO = new TournamentDTO()
            {
                ID = "",
                Name = "Test Tournament",
                OrganisationID = "null",
                UserID = "021d968c-b151-4399-903e-19d867f08e5a",
                Size = 32,
                Prize = 0,
                BuyIn = 1,
                Game = Games.CounterStrike,
                StartTime = new DateTime(2021, 2, 1, 12, 00, 00),
                Status = TourneyStatus.Planned,
                TeamSize = 1
            };

            //Act
            var result = tournament.CreateTournament(tournamentDTO);

            //Assert
            Assert.AreEqual(TournamentManagerErrorCodes.BuyInMoreOrEqualToPrize, result);
        }
        [TestMethod()]
        public void CreateTournament_PrizeLessThanUserCurrency_Test()
        {
            //Arrange
            TournamentManager tournament = new TournamentManager("Test");
            TournamentDTO tournamentDTO = new TournamentDTO()
            {
                ID = "",
                Name = "Test Tournament",
                OrganisationID = "null",
                UserID = "021d968c-b151-4399-903e-19d867f08e5a",
                Size = 32,
                Prize = 9,
                BuyIn = 0,
                Game = Games.CounterStrike,
                StartTime = new DateTime(2021, 2, 1, 12, 00, 00),
                Status = TourneyStatus.Planned,
                TeamSize = 1
            };

            //Act
            var result = tournament.CreateTournament(tournamentDTO);

            //Assert
            Assert.AreEqual(TournamentManagerErrorCodes.NoError, result);
        }
        [TestMethod()]
        public void CreateTournament_PrizeMoreThanUserCurrency_Test()
        {
            //Arrange
            TournamentManager tournament = new TournamentManager("Test");
            TournamentDTO tournamentDTO = new TournamentDTO()
            {
                ID = "",
                Name = "Test Tournament",
                OrganisationID = "null",
                UserID = "021d968c-b151-4399-903e-19d867f08e5a",
                Size = 32,
                Prize = 11,
                BuyIn = 0,
                Game = Games.CounterStrike,
                StartTime = new DateTime(2021, 2, 1, 12, 00, 00),
                Status = TourneyStatus.Planned,
                TeamSize = 1
            };

            //Act
            var result = tournament.CreateTournament(tournamentDTO);

            //Assert
            Assert.AreEqual(TournamentManagerErrorCodes.NotEnoughMoney, result);
        }
    }
}