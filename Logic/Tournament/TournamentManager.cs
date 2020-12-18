using System;
using System.Collections.Generic;
using System.Text;
using Model;
using DAL.Tournament;
using IdGenerator;
using Interface.Tournament;

namespace Logic.Tournament
{
    public enum TournamentErrorCodes
    {
        UnexpectedError = -1,
        NoError = 0,
        BuyInLessOrEqualToPrize = 1,

    }
    public class TournamentManager
    {
        private ITournamentDB tournamentDB = new TournamentDB();
        Generator idGenerator = new Generator();
        public TournamentErrorCodes CreateTournament(TournamentDTO tournamentDTO)
        {
            if(tournamentDTO.BuyIn > 0 && tournamentDTO.BuyIn >= tournamentDTO.Prize)
            {
                return TournamentErrorCodes.BuyInLessOrEqualToPrize;
            }
            tournamentDTO.ID = idGenerator.GenerateID(12);
            if(tournamentDTO.OrganisationID == "null")
            {
                tournamentDTO.OrganisationID = null;
            }
            tournamentDB.CreateTournament(tournamentDTO);
            return TournamentErrorCodes.NoError;
        }

        public List<TournamentDTO> Get10NextTournaments()
        {
            var tournaments = tournamentDB.FindAllFutureTournaments();
            if (tournaments.Count <= 10)
            {
                return tournaments;
            }
            tournaments.RemoveRange(10, tournaments.Count);
            return tournaments;
        }
    }
}
