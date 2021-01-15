using Interface;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestDAL.Team
{
    public class TestTeamDB : ITeamDB, ITeamCollectionDB
    {
        public bool AddPlayerToTeam(string PlayerID, string TeamID, int Role)
        {
            return true;
        }

        public bool CreateTeam(TeamDTO teamDTO)
        {
            return true;
        }

        public bool EditTeam(TeamDTO teamDTO)
        {
            return true;
        }

        public List<TeamDTO> FindAllTeams()
        {
            List<TeamDTO> TestList = new List<TeamDTO>
            {
                new TeamDTO()
                {
                    IsPrivate = false,
                    TeamID = "1234asdf",
                    Country = Countries.Belgium,
                    MinimumAge = 16,
                    TeamName = "Belgisch Team",
                    Description = "Een belgisch test team",
                    Language = Languages.Dutch,
                    MinimumElo = 1000,
                    PlayedGame = Games.CallOfDuty
                },
                new TeamDTO()
                {
                    IsPrivate = true,
                    TeamID = "1235asdg",
                    Country = Countries.Netherlands,
                    MinimumAge = 18,
                    TeamName = "Nederlands Team",
                    Description = "Een Nederlands test team",
                    Language = Languages.Dutch,
                    MinimumElo = 1500,
                    PlayedGame = Games.CounterStrike
                }
            };
            return TestList;
        }

        public TeamDTO FindTeamByID(string ID)
        {
            return new TeamDTO()
            {
                IsPrivate = false,
                TeamID = ID,
                Country = Countries.Belgium,
                MinimumAge = 16,
                TeamName = "FindTeamByID Team",
                Description = "Een FindTeamByID test team",
                Language = Languages.Dutch,
                MinimumElo = 1000,
                PlayedGame = Games.CallOfDuty
            };
        }

        public TeamDTO FindTeamByName(string Name)
        {
            if(Name == "A Used Name")
            {
                return new TeamDTO()
                {
                    IsPrivate = false,
                    TeamID = "1234asdf",
                    Country = Countries.Belgium,
                    MinimumAge = 16,
                    TeamName = "A Used Name",
                    Description = "Een belgisch test team",
                    Language = Languages.Dutch,
                    MinimumElo = 1000,
                    PlayedGame = Games.CallOfDuty
                };
            }
            return null;
        }

        public TeamDTO FindTeamByUser(string userID)
        {
            return new TeamDTO()
            {
                IsPrivate = false,
                TeamID = "1234asdf",
                Country = Countries.Belgium,
                MinimumAge = 16,
                TeamName = "FindTeamByUser Team",
                Description = "Een FindTeamByUser test team",
                Language = Languages.Dutch,
                MinimumElo = 1000,
                PlayedGame = Games.CallOfDuty
            };
        }

        public List<string> GetMembers(string teamID)
        {
            List<string> TestList = new List<string>()
            {
                "Tijn",
                "Danillo",
                "Luuk"
            };
            return TestList;
        }

        public List<TeamMatchResultDTO> GetPreviousResults(string teamID)
        {
            List<TeamMatchResultDTO> TestList = new List<TeamMatchResultDTO>();
            TestList.Add(new TeamMatchResultDTO
            {
                OpponentName = "Danillo",
                ScoreOpponent = 5,
                ScoreSelf = 16,
                Won = true,
            });
            return TestList;
        }

        public TeamRoles GetRole(string userID, string teamID)
        {
            return TeamRoles.Owner;
        }

        public int GetUserTeamRole(string UserID)
        {
            return 1;
        }
    }
}
