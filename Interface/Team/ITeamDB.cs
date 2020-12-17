using Model;
using System;
using System.Collections.Generic;

namespace Interface
{
    public interface ITeamDB
    {
        bool AddPlayerToTeam(string PlayerID, string TeamID, int Role);
        bool EditTeam(TeamDTO teamDTO);
        List<string> GetMembers(string teamID);
    }
}
