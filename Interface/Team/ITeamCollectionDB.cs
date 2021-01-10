using Model;
using System.Collections.Generic;

namespace Interface
{
    public interface ITeamCollectionDB
    {
        bool CreateTeam(TeamDTO teamDTO);
        TeamDTO FindTeamByID(string ID);
        TeamDTO FindTeamByName(string Name);
        TeamDTO FindTeamByUser(string userID);
        int GetUserTeamRole(string UserID);
        List<TeamDTO> FindAllTeams();
    }
}