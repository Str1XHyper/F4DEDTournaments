using Model;
using System.Collections.Generic;

namespace DAL
{
    public interface ITeamCollectionDB
    {
        bool AddPlayerToTeam(string PlayerID, string TeamID, int Role);
        bool CreateTeam(TeamDTO teamDTO);
        bool EditTeam(TeamDTO teamDTO);
        TeamDTO FindTeamByID(string ID);
        TeamDTO FindTeamByName(string Name);
        TeamDTO FindTeamByUser(string userID);
        int GetUserTeamRole(string UserID);
        List<TeamDTO> FindAllTeams();
    }
}