using Model;
using System.Collections.Generic;

namespace Interface.Tournament
{
    public interface ITournamentManagerDB
    {
        bool CreateTournament(TournamentDTO tournamentDTO);
        List<TournamentDTO> FindAllFutureTournaments();
        List<TournamentDTO> FindAllTournaments();
        TournamentDTO FindTournamentByID(string ID);
        TournamentDTO FindTournamentByName(string Name);
        List<TournamentDTO> FindActiveTournaments();
    }
}