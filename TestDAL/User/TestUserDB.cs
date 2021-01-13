using Interface.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestDAL.User
{
    public class TestUserDB : IUserDB
    {
        public int GetCurrency(string UserID)
        {
            return 10;
        }
    }
}
