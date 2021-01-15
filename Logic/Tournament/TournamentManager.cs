using System;
using System.Collections.Generic;
using System.Text;
using Model;
using IdGenerator;
using Interface.Tournament;
using Interface.User;
using DalFactory;

namespace Logic.Tournament
{
    public enum TournamentManagerErrorCodes
    {
        UnexpectedError = -1,
        NoError = 0,
        BuyInMoreOrEqualToPrize = 1,
        NoHost = 2,
        NotEnoughMoney = 3,
    }
    public class TournamentManager
    {
        private ITournamentManagerDB tournamentDB = TournamentFactory.GetTournamentManagerDB("release");
        private IUserDB userDB = UserFactory.GetUserDB("release");
        Generator idGenerator = new Generator();

        public TournamentManager(string DataSource)
        {
            tournamentDB = TournamentFactory.GetTournamentManagerDB(DataSource);
            userDB = UserFactory.GetUserDB(DataSource);
        }
        public TournamentManager()
        {

        }

        public TournamentManagerErrorCodes CreateTournament(TournamentDTO tournamentDTO)
        {
            if(tournamentDTO.BuyIn > 0 && (tournamentDTO.BuyIn * tournamentDTO.Size ) >= tournamentDTO.Prize)
            {
                return TournamentManagerErrorCodes.BuyInMoreOrEqualToPrize;
            }
            if (tournamentDTO.OrganisationID == "null" && tournamentDTO.UserID == "null")
            {
                return TournamentManagerErrorCodes.NoHost;
            }
            tournamentDTO.ID = idGenerator.GenerateID(12);
            if(tournamentDTO.OrganisationID == "null")
            {
                tournamentDTO.OrganisationID = null;

                int extraCost = tournamentDTO.Prize - (tournamentDTO.BuyIn * tournamentDTO.Size);
                if (userDB.GetCurrency(tournamentDTO.UserID) < extraCost)
                {
                    return TournamentManagerErrorCodes.NotEnoughMoney;
                }
            } 
            else
            {
                tournamentDTO.UserID = null;
            }
            tournamentDB.CreateTournament(tournamentDTO);
            return TournamentManagerErrorCodes.NoError;
        }

        public List<Tournament> Get10NextTournaments()
        {
            var tournamentDTOs = tournamentDB.FindAllFutureTournaments();
            List<Tournament> tournaments = new List<Tournament>();
            foreach(TournamentDTO dto in tournamentDTOs)
            {
                tournaments.Add(new Tournament(dto));
            }

            if (tournaments.Count <= 10)
            {
                return tournaments;
            }
            tournaments.RemoveRange(10, tournaments.Count);
            return tournaments;
        }

        public Tournament GetTournamentById(string ID)
        {
            if(ID == string.Empty || ID== null)
            {
                return null;
            }
            Tournament tournament = new Tournament(tournamentDB.FindTournamentByID(ID));
            return tournament;
        }

        public List<Tournament> GetActiveTournaments()
        {
            var tournamentDTOs = tournamentDB.FindActiveTournaments();
            List<Tournament> tournaments = new List<Tournament>();
            foreach (TournamentDTO dto in tournamentDTOs)
            {
                tournaments.Add(new Tournament(dto));
            }
            return tournaments;
        }
    }
}
