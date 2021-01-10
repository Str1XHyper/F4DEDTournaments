using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interface.Tournament
{
    public interface ITournamentDB
    {
        bool AddTeamToTournament(string TeamID, string TournamentID);
        bool AddUserToTournament(string UserID, string TournamentID);
        bool EditTournament(TournamentDTO tournamentDTO);
        string[] GetUsers(string TournamentID);
    }
}
