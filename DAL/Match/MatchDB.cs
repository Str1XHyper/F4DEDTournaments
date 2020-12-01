using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Match
{
    public class MatchDB
    {
        public bool CreateMatch(MatchDTO matchDTO)
        {
            List<string[]> param = new List<string[]>()
            {
                new string[] {"@ID", matchDTO.ID},
                new string[] {"@Game", ((int)matchDTO.Game).ToString()},
                new string[] {"@PlayDate", matchDTO.PlayDate.ToString("YYYY-MM-DD hh:mm:ss")},
                new string[] {"@ScoreTeam1", matchDTO.ScoreTeam1.ToString()},
                new string[] {"@ScoreTeam2", matchDTO.ScoreTeam2.ToString()},
            };
            var result = SQLConnection.ExecuteNonSearchQueryParameters("INSERT INTO Match (ID,Game,PlayDate,ScoreTeam1,ScoreTeam2) VALUES (@ID,@Game,@PlayDate,@ScoreTeam1,@ScoreTeam2)", param);
            return result;
        }
    }
}
