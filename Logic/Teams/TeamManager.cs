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

    public class TeamManager
    {
        TeamDB teamDB = new TeamDB();
        Generator idGenerator = new Generator();
        public TeamErrorCodes CreateTeam(TeamDTO teamDTO)
        {
            var result = teamDB.FindTeamByName(teamDTO.TeamName);
            if(result.Count > 0)
            {
                return TeamErrorCodes.NameAlreadyExists;
            }
            teamDTO.TeamID = idGenerator.GenerateID(12);
            teamDB.CreateTeam(teamDTO);
            return TeamErrorCodes.NoError;
        }
    }
}
