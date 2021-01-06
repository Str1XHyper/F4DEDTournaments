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
        List<Team> teams = new List<Team>();
        Generator idGenerator = new Generator();

        public TeamManager()
        {
            var AllTeamDTOs = teamManagerDB.FindAllTeams();
            foreach (TeamDTO teamDTO in AllTeamDTOs)
            {
                var team = new Team(teamDTO);
                teams.Add(team);
            }
        }

        public TeamErrorCodes CreateTeam(TeamDTO teamDTO,string UserID)
        {
            var result = teamManagerDB.FindTeamByName(teamDTO.TeamName);
            if(result != null)
            {
                return TeamErrorCodes.NameAlreadyExists;
            }
            teamDTO.TeamID = idGenerator.GenerateID(12);
            teamManagerDB.CreateTeam(teamDTO);
            Team team = new Team(teamDTO);
            teams.Add(team);
            team.AddPlayer(UserID, TeamRoles.Owner);
            return TeamErrorCodes.NoError;
        }
        public Team GetTeamByID(string ID)
        {
            var team = teams.Find(x => x.TeamID == ID);
            return team;
        }

        public UserTeamDTO GetUserTeam(string UserID)
        {
            var TeamDTO = teamManagerDB.FindTeamByUser(UserID);
            var team = teams.Find(x => x.TeamID == TeamDTO.TeamID);
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
            if(TeamDTO == null)
            {
                return null;
            }
            var team = teams.Find(x => x.TeamID == TeamDTO.TeamID);
            return team;
        }

        public List<Team> GetTop10Teams()
        {
            if(teams.Count <= 10)
            {
                return teams;
            }
            List<Team> top10 = teams;
            top10.RemoveRange(10, teams.Count);
            return top10;
        }

    }
}
