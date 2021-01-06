using Interface;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{

    public class TeamDB : ITeamCollectionDB, ITeamDB
    {
        public bool CreateTeam(TeamDTO teamDTO)
        {
            List<string[]> param = new List<string[]>()
            {
                new string[] { "@TeamID", teamDTO.TeamID},
                new string[] { "@TeamName", teamDTO.TeamName},
                new string[] { "@MinimumElo", teamDTO.MinimumElo.ToString()},
                new string[] { "@IsPrivate", (Convert.ToInt32(teamDTO.IsPrivate)).ToString()},
                new string[] { "@Description", teamDTO.Description},
                new string[] { "@CountryOfOrigin", ((int)teamDTO.Country).ToString()},
                new string[] { "@SpokenLanguage", ((int)teamDTO.Language).ToString()},
                new string[] { "@MinimumAge", teamDTO.MinimumAge.ToString()},
                new string[] { "@PlayedGame", ((int)teamDTO.PlayedGame).ToString()},
            };
            var result = SQLConnection.ExecuteNonSearchQueryParameters($"INSERT INTO Teams (TeamID,TeamName,MinimumElo,IsPrivate,Description,CountryOfOrigin,SpokenLanguage,MinimumAge,PlayedGame) VALUES (@TeamID, @TeamName,@MinimumElo,@IsPrivate,@Description,@CountryOfOrigin,@SpokenLanguage,@MinimumAge,@PlayedGame);", param);
            return result;
        }

        public bool EditTeam(TeamDTO teamDTO)
        {
            List<string[]> param = new List<string[]>()
            {
                new string[] { "@TeamID", teamDTO.TeamID},
                new string[] { "@TeamName", teamDTO.TeamName},
                new string[] { "@MinimumElo", teamDTO.MinimumElo.ToString()},
                new string[] { "@IsPrivate", (Convert.ToInt32(teamDTO.IsPrivate)).ToString()},
                new string[] { "@Description", teamDTO.Description},
                new string[] { "@CountryOfOrigin", ((int)teamDTO.Country).ToString()},
                new string[] { "@SpokenLanguage", ((int)teamDTO.Language).ToString()},
                new string[] { "@MinimumAge", teamDTO.MinimumAge.ToString()},
                new string[] { "@PlayedGame", ((int)teamDTO.PlayedGame).ToString()},
            };
            return SQLConnection.ExecuteNonSearchQueryParameters($"UPDATE Teams SET `TeamName` = @TeamName, `MinimumElo` = @MinimumElo, `IsPrivate` = @IsPrivate, `Description` = @Description, `CountryOfOrigin` = @CountryOfOrigin, `SpokenLanguage` = @SpokenLanguage, `MinimumAge` = @MinimumAge, `PlayedGame` = @PlayedGame WHERE TeamID = @TeamID", param);
        }

        public TeamDTO FindTeamByName(string Name)
        {
            List<string[]> param = new List<string[]>()
            {
                new string[] { "@TeamName", Name},
            };
            var result = SQLConnection.ExecuteSearchQueryParameters($"SELECT * FROM Teams WHERE `TeamName` = @TeamName",param);
            if (result.Count == 0)
            {
                return null;
            }
            return GenerateDTOFromRow(result[0]);
        }

        public TeamDTO FindTeamByID(string ID)
        {
            List<string[]> param = new List<string[]>()
            {
                new string[] { "@TeamID", ID},
            };
            var result = SQLConnection.ExecuteSearchQueryParameters($"SELECT * FROM Teams Where TeamID = @TeamID", param);
            return GenerateDTOFromRow(result[0]);
        }
        public List<TeamDTO> FindAllTeams()
        {
            var result = SQLConnection.ExecuteSearchQuery($"SELECT * FROM Teams");
            return GenerateDTOsFromRows(result);
        }
        public TeamDTO FindTeamByUser(string userID)
        {
            List<string[]> param = new List<string[]>()
            {
                new string[] { "@UserID", userID},
            };
            var TeamID = SQLConnection.ExecuteSearchQueryParameters("SELECT TeamID FROM UserTeams Where UserID = @UserID", param);
            if(TeamID.Count == 0)
            {
                return null;
            }
            param = new List<string[]>()
            {
                new string[] { "@TeamID", TeamID[0][0]},
            };
            var Team = SQLConnection.ExecuteSearchQueryParameters("SELECT * FROM Teams Where TeamID = @TeamID", param);
            return GenerateDTOFromRow(Team[0]);
        }
        public int GetUserTeamRole(string UserID)
        {
            List<string[]> param = new List<string[]>()
            {
                new string[] { "@UserID", UserID},
            };
            var Role = SQLConnection.ExecuteSearchQueryParameters("SELECT Role FROM UserTeams Where UserID = @UserID", param);
            return Convert.ToInt32(Role[0][0]);
        }

        private TeamDTO GenerateDTOFromRow(string[] row)
        {

            TeamDTO teamDTO = new TeamDTO()
            {
                TeamID = row[0],
                TeamName = row[1],
                MinimumElo = Convert.ToInt32(row[2]),
                IsPrivate = Convert.ToBoolean(row[3]),
                Description = row[4],
                Country = (Countries)Convert.ToInt32(row[5]),
                Language = (Languages)Convert.ToInt32(row[6]),
                MinimumAge = Convert.ToInt32(row[7]),
                PlayedGame = (Games)Convert.ToInt32(row[8])
            };
            return teamDTO;
        }

        private List<TeamDTO> GenerateDTOsFromRows(List<string[]> rows)
        {
            List<TeamDTO> teamList = new List<TeamDTO>();
            foreach (string[] row in rows)
            {
                teamList.Add(new TeamDTO()
                {
                    TeamID = row[0],
                    TeamName = row[1],
                    MinimumElo = Convert.ToInt32(row[2]),
                    IsPrivate = Convert.ToBoolean(row[3]),
                    Description = row[4],
                    Country = (Countries)Convert.ToInt32(row[5]),
                    Language = (Languages)Convert.ToInt32(row[6]),
                    MinimumAge = Convert.ToInt32(row[7]),
                    PlayedGame = (Games)Convert.ToInt32(row[8])
                });
            }

            return teamList;
        }

        public bool AddPlayerToTeam(string PlayerID, string TeamID, int Role)
        {
            List<string[]> param = new List<string[]>()
            {
                new string[] { "@PlayerID", PlayerID},
                new string[] { "@TeamID", TeamID},
                new string[] { "@Role", Role.ToString()},
            };
            var result = SQLConnection.ExecuteNonSearchQueryParameters($"INSERT INTO UserTeams (UserID,TeamID,Role) VALUES (@PlayerID,@TeamID,@Role)", param);
            return result;
        }

        public List<string> GetMembers(string teamID)
        {
            List<string[]> param = new List<string[]>()
            {
                new string[] { "@TeamID", teamID},
            };
            List<string[]> Members = SQLConnection.ExecuteSearchQueryParameters(
                @"SELECT Users.UserName 
                FROM UserTeams UT 
                INNER JOIN AspNetUsers Users 
                ON UT.UserID = Users.Id 
                WHERE UT.TeamID = @TeamID", param);
            List<string> returnResult = new List<string>();
            foreach(string[] row in Members)
            {
                returnResult.Add(row[0]);
            }
            return returnResult;
        }

        public List<TeamMatchResultDTO> GetPreviousResults(string teamID)
        {
            List<string[]> param = new List<string[]>
            {
                new string[] {"@TeamID", teamID}
            }; 
            //SELECT T.TeamName FROM TeamMatch TM INNER JOIN Teams T ON T.TeamID = TM.TeamID WHERE MatchID IN (SELECT MatchID FROM TeamMatch TM WHERE TeamID = @TeamID) AND T.TeamID <> @TeamID
            var result = SQLConnection.ExecuteSearchQueryParameters(
                @"SELECT T.TeamName AS Enemy, TM.Score AS EnemyScore, (SELECT Score FROM TeamMatch WHERE TeamID = @TeamID) AS OwnScore
                    FROM (TeamMatch TM
                    INNER JOIN Teams T ON T.TeamID = TM.TeamID)
                    INNER JOIN `Match` M ON M.ID = TM.MatchID
                    WHERE MatchID IN
                    (
                    SELECT MatchID
                    FROM TeamMatch TM
                    WHERE TeamID = @TeamID
                    ) AND T.TeamID <> @TeamID
                    ORDER BY M.PlayDate", param);
            List<TeamMatchResultDTO> returnList = new List<TeamMatchResultDTO>();
            foreach(var row in result)
            {
                TeamMatchResultDTO resultDTO = new TeamMatchResultDTO()
                {
                    OpponentName = row[0],
                    ScoreOpponent = Convert.ToInt32(row[1]),
                    ScoreSelf = Convert.ToInt32(row[2]),
                    Won = Convert.ToInt32(row[1]) < Convert.ToInt32(row[2]),
                };

                resultDTO.Won = resultDTO.ScoreSelf > resultDTO.ScoreOpponent;
                returnList.Add(resultDTO);
            }
            return returnList;
        }

        public TeamRoles GetRole(string userID, string teamID)
        {
            List<string[]> param = new List<string[]>
            {
                new string[] {"@UserID", userID },
                new string[] {"@TeamID", teamID}
            };
            var result = SQLConnection.ExecuteSearchQueryParameters("SELECT Role FROM UserTeams WHERE UserID = @UserID AND TeamID = @TeamID", param);
            var role = result.Count > 0 ? (TeamRoles)Convert.ToInt32(result[0]) : TeamRoles.NonMember;
            return role;
        }
    }
}
