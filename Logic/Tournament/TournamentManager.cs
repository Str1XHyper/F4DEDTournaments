using System;
using System.Collections.Generic;
using System.Text;
using Model;
using DAL.Tournament;
using IdGenerator;

namespace Logic.Tournament
{
    public enum TournamentErrorCodes
    {
        NoError = 0,
        
    }
    public class TournamentManager
    {
        private TournamentDB tournamentDB = new TournamentDB();
        Generator idGenerator = new Generator();
        public TournamentErrorCodes CreateTeam(TournamentDTO tournamentDTO)
        {
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
