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
        UnknownException = -1,
        NoError = 0,
        NameAlreadyExists = 1,
    }

    public class TeamManager
    {
<<<<<<< HEAD
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

=======
        ITeamCollectionDB teamManagerDB = new TeamManagerDB();
        Generator idGenerator = new Generator();
>>>>>>> parent of daca28f... Added Mailing System and interface segregation
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

        public TeamDTO GetTeamByID(string ID) => teamManagerDB.FindTeamByID(ID);

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

<<<<<<< HEAD
        public Team GetTeamByUser(string UserID)
        {
            var TeamDTO = teamManagerDB.FindTeamByUser(UserID);
            var team = teams.Find(x => x.TeamID == TeamDTO.TeamID);
            return team;
        }

        public List<Team> GetTop10Teams()
=======
        public List<TeamDTO> GetTop10Teams()
>>>>>>> parent of daca28f... Added Mailing System and interface segregation
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
