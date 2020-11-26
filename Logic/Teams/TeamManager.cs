using Model;
using System;
using System.Collections.Generic;
using System.Text;
using IdGenerator;
using DAL;

namespace Logic
{
    public enum TeamErrorCodes
    {
        NoError = 0,
        NameAlreadyExists = 1,
    }

    public enum TeamRoles
    {
        Member = 0,
        Admin = 1,
        Owner = 2
    }

    public class TeamManager
    {
        ITeamCollectionDB teamManagerDB = new TeamManagerDB();
        Generator idGenerator = new Generator();
        public TeamErrorCodes CreateTeam(TeamDTO teamDTO,string UserID)
        {
            var result = teamManagerDB.FindTeamByName(teamDTO.TeamName);
            if(result != null)
            {
                return TeamErrorCodes.NameAlreadyExists;
            }
            teamDTO.TeamID = idGenerator.GenerateID(12);
            teamManagerDB.CreateTeam(teamDTO);
            teamManagerDB.AddPlayerToTeam(UserID, teamDTO.TeamID, (int) TeamRoles.Owner);
            return TeamErrorCodes.NoError;
        }

        public TeamDTO GetTeamByID(string ID)
        {
            return teamManagerDB.FindTeamByID(ID);
        }
    }
}
