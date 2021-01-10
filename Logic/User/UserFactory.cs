using DAL.User;
using Interface.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.User
{
    public static class UserFactory
    {
        public static IUserDB GetUserDB(string source)
        {
            switch (source.ToLower())
            {
                case "release":
                    return new UserDB();
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
