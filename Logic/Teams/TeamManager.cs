using Model;
using System;
using System.Collections.Generic;
using System.Text;
using IdGenerator;
using DAL;

namespace Logic.Teams
{
    public enum TeamErrorCodes
    {
        UnknownException = -1,
        NoError = 0,
        NameAlreadyExists = 1,
    }

    public class TeamManager
    {
        ITeamCollectionDB teamManagerDB = new TeamDB();
        Generator idGenerator = new Generator();


        public TeamErrorCodes CreateTeam(TeamDTO teamDTO, string UserID)
        {

            var result = teamManagerDB.FindTeamByName(teamDTO.TeamName);
            if (result != null)
            {
                return TeamErrorCodes.NameAlreadyExists;
            }
            teamDTO.TeamID = idGenerator.GenerateID(12);
            teamManagerDB.CreateTeam(teamDTO);
            Team team = new Team(teamDTO);
            team.AddPlayer(UserID, TeamRoles.Owner);
            return TeamErrorCodes.NoError;
        }
        public Team GetTeamByID(string ID)
        {
            var teamDTO = teamManagerDB.FindTeamByID(ID);
            if (teamDTO == null)
            {
                return null;
            }
            Team team = new Team(teamDTO);
            return team;
        }


        public UserTeamDTO GetUserTeam(string UserID)
        {
            var TeamDTO = teamManagerDB.FindTeamByUser(UserID);
            var team = new Team(TeamDTO);
            var role = teamManagerDB.GetUserTeamRole(UserID);
            UserTeamDTO userTeamDTO = new UserTeamDTO()
            {
                Team = team,
                Role = (TeamRoles)role
            };
            return userTeamDTO;
        }

        public Team GetTeamByUser(string UserID)
        {
            var TeamDTO = teamManagerDB.FindTeamByUser(UserID);
            if (TeamDTO == null)
            {
                return null;
            }
            var team = new Team(TeamDTO);
            return team;
        }

        public List<Team> GetTop10Teams()
        {
            var teamDTOs = teamManagerDB.FindAllTeams();
            var teams = new List<Team>();
            foreach(TeamDTO teamDTO in teamDTOs)
            {
                teams.Add(new Team(teamDTO));
            }
            if (teams.Count <= 10)
            {
                return teams;
            }
            teams.RemoveRange(10, teams.Count);
            return teams;
        }



    }
}
