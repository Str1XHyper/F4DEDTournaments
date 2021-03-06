﻿using Model;
using System;
using System.Collections.Generic;
using System.Text;
using IdGenerator;
using Interface;
using DalFactory;

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
        ITeamCollectionDB teamManagerDB = TeamFactory.GetTeamManagerDB("release");
        Generator idGenerator = new Generator();
        private string source;

        public TeamManager(string source)
        {
            this.source = source;
            teamManagerDB = TeamFactory.GetTeamManagerDB(source);
        }

        public TeamManager()
        {
            source = "release";
            teamManagerDB = TeamFactory.GetTeamManagerDB("release");
        }


        public TeamErrorCodes CreateTeam(TeamDTO teamDTO, string UserID)
        {

            var result = teamManagerDB.FindTeamByName(teamDTO.TeamName);
            if (result != null)
            {
                return TeamErrorCodes.NameAlreadyExists;
            }
            teamDTO.TeamID = idGenerator.GenerateID(12);
            teamManagerDB.CreateTeam(teamDTO);
            Team team = new Team(teamDTO,source);
            team.AddPlayer(UserID, TeamRoles.Owner);
            return TeamErrorCodes.NoError;
        }
        public Team GetTeamByID(string ID)
        {
            if(ID == null || ID == string.Empty)
            {
                return null;
            }
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
            if(UserID == null || UserID == string.Empty)
            {
                return null;
            }
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
            if (UserID == null || UserID == string.Empty)
            {
                return null;
            }
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
