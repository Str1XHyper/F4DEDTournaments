using DAL.User;
using Interface.User;
using System;
using System.Collections.Generic;
using System.Text;
using TestDAL.User;

namespace DalFactory
{
    public static class UserFactory
    {
        public static IUserDB GetUserDB(string source)
        {
            switch (source.ToLower())
            {
                case "release":
                    return new UserDB();
                case "test":
                    return new TestUserDB();
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
