using DAL;
using Interface;
using System;
using System.Collections.Generic;
using System.Text;
using TestDAL.Team;

namespace Logic.Teams
{
    public static class TeamFactory
    {
        public static ITeamDB GetTeamDB(string source)
        {
            switch (source.ToLower())
            {
                case "release":
                    return new TeamDB();
                case "test":
                    return new TestTeamDB();
                default:
                    throw new NotImplementedException();
            }
        }

        public static ITeamCollectionDB GetTeamManagerDB(string source)
        {
            switch (source.ToLower())
            {
                case "release":
                    return new TeamDB();
                case "test":
                    return new TestTeamDB();
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
