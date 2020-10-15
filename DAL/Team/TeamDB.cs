using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class TeamDB
    {
        public bool CreateTeam(TeamDTO teamDTO)
        {
            var result = SQLConnection.ExecuteNonSearchQuery($"INSERT INTO Teams (TeamID,TeamName,MinimumElo,IsPrivate,Description,CountryOfOrigin,SpokenLanguage,MinimumAge,PlayedGame) VALUES ('{teamDTO.TeamID}', '{teamDTO.TeamName}','{teamDTO.MinimumElo}','{teamDTO.IsPrivate}','{teamDTO.Description}','{teamDTO.Country}','{teamDTO.Language}','{teamDTO.MinimumAge}','{teamDTO.PlayedGame}'); ");
            return result;
        }

        public List<string[]> FindTeamByName(string Name)
        {
            return SQLConnection.ExecuteSearchQuery($"SELECT * FROM Teams WHERE `TeamName` = '{Name}'");
        }

        public List<string[]> FindTeamByID(int ID)
        {
            return SQLConnection.ExecuteSearchQuery($"SELECT * FROM Teams Where TeamID = {ID}");
        }
    }
}
