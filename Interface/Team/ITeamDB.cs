using Model;
using System;

namespace Interface
{
    public interface ITeamDB
    {
        bool AddPlayerToTeam(string PlayerID, string TeamID, int Role);
        bool EditTeam(TeamDTO teamDTO);
    }
}
