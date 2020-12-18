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
            };
            var result = SQLConnection.ExecuteNonSearchQueryParameters("INSERT INTO Match (ID,Game,PlayDate) VALUES (@ID,@Game,@PlayDate)", param);
            return result;
        }
    }
}
