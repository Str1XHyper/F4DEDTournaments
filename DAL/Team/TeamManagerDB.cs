using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class TeamManagerDB : ITeamCollectionDB
    {
        public bool CreateTeam(TeamDTO teamDTO)
        {
            var result = SQLConnection.ExecuteNonSearchQuery($"INSERT INTO Teams (TeamID,TeamName,MinimumElo,IsPrivate,Description,CountryOfOrigin,SpokenLanguage,MinimumAge,PlayedGame) VALUES ('{teamDTO.TeamID}', '{teamDTO.TeamName}','{teamDTO.MinimumElo}','{teamDTO.IsPrivate}','{teamDTO.Description}','{(int)teamDTO.Country}','{(int)teamDTO.Language}','{teamDTO.MinimumAge}','{(int)teamDTO.PlayedGame}'); ");
            return result;
        }

        public bool EditTeam(TeamDTO teamDTO)
        {
            return SQLConnection.ExecuteNonSearchQuery($"UPDATE Teams SET `TeamName` = '{teamDTO.TeamName}', `MinimumElo` = '{teamDTO.MinimumElo}', `IsPrivate` = '{teamDTO.IsPrivate}', `Description` = '{teamDTO.Description}', `CountryOfOrigin` = '{(int)teamDTO.Country}', `SpokenLanguage` = '{(int)teamDTO.Language}', `MinimumAge` = '{teamDTO.MinimumAge}', `PlayedGame` = '{(int)teamDTO.PlayedGame}' ");
        }

        public TeamDTO FindTeamByName(string Name)
        {
            var result = SQLConnection.ExecuteSearchQuery($"SELECT * FROM Teams WHERE `TeamName` = '{Name}'");
            if (result.Count == 0)
            {
                return null;
            }
            return GenerateDTOFromRow(result[0]);
        }

        public TeamDTO FindTeamByID(string ID)
        {
            var result = SQLConnection.ExecuteSearchQuery($"SELECT * FROM Teams Where TeamID = '{ID}'");
            return GenerateDTOFromRow(result[0]);
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
                PlayedGame = (Games)Convert.ToInt32(row[7])
            };
            return teamDTO;
        }

        private List<TeamDTO> GenerateDTOsFromRows(List<string[]> rows)
        {
            List<TeamDTO> teamDTOs = new List<TeamDTO>();
            foreach (string[] row in rows)
            {
                teamDTOs.Add(new TeamDTO()
                {
                    TeamID = row[0],
                    TeamName = row[1],
                    MinimumElo = Convert.ToInt32(row[2]),
                    IsPrivate = Convert.ToBoolean(row[3]),
                    Description = row[4],
                    Country = (Countries)Convert.ToInt32(row[5]),
                    Language = (Languages)Convert.ToInt32(row[6]),
                    MinimumAge = Convert.ToInt32(row[7]),
                    PlayedGame = (Games)Convert.ToInt32(row[7])
                });
            }

            return teamDTOs;
        }

        public bool AddPlayerToTeam(string PlayerID, string TeamID, int Role)
        {
            throw new NotImplementedException();
        }
    }
}
