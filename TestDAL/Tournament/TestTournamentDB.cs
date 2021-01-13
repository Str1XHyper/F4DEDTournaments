using Interface.Tournament;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestDAL.Tournament
{
    public class TestTournamentDB : ITournamentDB, ITournamentManagerDB
    {
        public bool AddTeamToTournament(string TeamID, string TournamentID)
        {
            throw new NotImplementedException();
        }

        public bool AddUserToTournament(string UserID, string TournamentID)
        {
            throw new NotImplementedException();
        }

        public bool CreateTournament(TournamentDTO tournamentDTO)
        {
            return true;
        }

        public bool EditTournament(TournamentDTO tournamentDTO)
        {
            throw new NotImplementedException();
        }

        public List<TournamentDTO> FindActiveTournaments()
        {
            List<TournamentDTO> tournamentDTOs = new List<TournamentDTO>()
            {
                new TournamentDTO()
                {
                    ID = "A1B2C3D4",
                    Name = "Active Test Tournament",
                    OrganisationID = "null",
                    UserID = "021d968c-b151-4399-903e-19d867f08e5a",
                    Size = 32,
                    Prize = 0,
                    BuyIn = 0,
                    Game = Games.CounterStrike,
                    StartTime = new DateTime(2021, 2, 1, 12, 00, 00),
                    Status = TourneyStatus.Active,
                    TeamSize = 1
                }
            };
            return tournamentDTOs;
        }

        public List<TournamentDTO> FindAllFutureTournaments()
        {
            List<TournamentDTO> tournamentDTOs = new List<TournamentDTO>()
            {
                new TournamentDTO()
                {
                    ID = "A1B2C3D4",
                    Name = "Planned Test Tournament",
                    OrganisationID = "null",
                    UserID = "021d968c-b151-4399-903e-19d867f08e5a",
                    Size = 32,
                    Prize = 0,
                    BuyIn = 0,
                    Game = Games.CounterStrike,
                    StartTime = new DateTime(2021, 2, 1, 12, 00, 00),
                    Status = TourneyStatus.Planned,
                    TeamSize = 1
                }
            };
            return tournamentDTOs;
        }

        public List<TournamentDTO> FindAllTournaments()
        {
            List<TournamentDTO> tournamentDTOs = new List<TournamentDTO>()
            {
                new TournamentDTO()
                {
                    ID = "A1B2C3D4",
                    Name = "Planned Test Tournament",
                    OrganisationID = "null",
                    UserID = "021d968c-b151-4399-903e-19d867f08e5a",
                    Size = 32,
                    Prize = 0,
                    BuyIn = 0,
                    Game = Games.CounterStrike,
                    StartTime = new DateTime(2021, 2, 1, 12, 00, 00),
                    Status = TourneyStatus.Planned,
                    TeamSize = 1
                },
                new TournamentDTO()
                {
                    ID = "A1B2C3D4",
                    Name = "Active Test Tournament",
                    OrganisationID = "null",
                    UserID = "021d968c-b151-4399-903e-19d867f08e5a",
                    Size = 32,
                    Prize = 0,
                    BuyIn = 0,
                    Game = Games.CounterStrike,
                    StartTime = new DateTime(2021, 2, 1, 12, 00, 00),
                    Status = TourneyStatus.Active,
                    TeamSize = 1
                }
            };
            return tournamentDTOs;
        }

        public TournamentDTO FindTournamentByID(string ID)
        {
            return
                   new TournamentDTO()
                   {
                       ID = ID,
                       Name = "Active Test Tournament",
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
        }

        public TournamentDTO FindTournamentByName(string Name)
        {
            return
                   new TournamentDTO()
                   {
                       ID = "A1B2C3D4",
                       Name = Name,
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
        }

        public string[] GetUsers(string TournamentID)
        {
            throw new NotImplementedException();
        }
    }
}
