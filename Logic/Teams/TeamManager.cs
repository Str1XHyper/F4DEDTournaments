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

        public TeamManager()
        {
            var AllTeamDTOs = teamManagerDB.FindAllTeams();
            foreach (TeamDTO teamDTO in AllTeamDTOs)
            {
                var team = new Team(teamDTO);
                teams.Add(team);
            }
        }

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
            var Team = teamManagerDB.FindTeamByUser(UserID);
            var role = teamManagerDB.GetUserTeamRole(UserID);
            UserTeamDTO userTeamDTO = new UserTeamDTO()
            {
                TeamID = Team.TeamID,
                TeamName = Team.TeamName,
                MinimumAge = Team.MinimumAge,
                MinimumElo = Team.MinimumElo,
                Country = Team.Country,
                Language = Team.Language,
                Description = Team.Description,
                IsPrivate = Team.IsPrivate,
                PlayedGame = Team.PlayedGame,
            };
            userTeamDTO.Role = (TeamRoles)role;
            return userTeamDTO;
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
