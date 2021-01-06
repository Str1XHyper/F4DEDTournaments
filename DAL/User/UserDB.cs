using Interface.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.User
{
    public class UserDB : IUserDB
    {
        public int GetCurrency(string UserID)
        {
            List<string[]> param = new List<string[]>()
            {
                new string[] { "@UserID", UserID},
            };
            var result = SQLConnection.ExecuteSearchQueryParameters("SELECT Currency FROM AspNetUsers WHERE Id = @UserID", param);
            return Convert.ToInt32(result[0][0]);
        }
    }
}
