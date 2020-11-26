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

        public List<TeamDTO> GetTop10Teams()
        {
            var teams = teamManagerDB.FindAllTeams();
            if(teams.Count <= 10)
            {
                return teams;
            }
            teams.RemoveRange(10, teams.Count);
            return teams;
        }
    }
}
