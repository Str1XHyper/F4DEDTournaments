using Model;
using System.Collections.Generic;

namespace Interface.Tournament
{
    public interface ITournamentManagerDB
    {
        bool AddTeamToTournament(string TeamID, string TournamentID);
        bool AddUserToTournament(string UserID, string TournamentID);
        bool CreateTournament(TournamentDTO tournamentDTO);
        bool EditTournament(TournamentDTO tournamentDTO);
        List<TournamentDTO> FindAllFutureTournaments();
        List<TournamentDTO> FindAllTournaments();
        TournamentDTO FindTournamentByID(string ID);
        TournamentDTO FindTournamentByName(string Name);
    }
}