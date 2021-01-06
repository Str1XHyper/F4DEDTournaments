using System;
using System.Collections.Generic;
using System.Text;

namespace Interface.Tournament
{
    public interface ITournamentDB
    {
        string[] GetUsers(string TournamentID);
    }
}
