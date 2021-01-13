using DAL.Match;
using Interface.Match;
using System;
using System.Collections.Generic;
using System.Text;

namespace DalFactory
{
    public static class MatchFactory
    {
        public static IMatchManagerDB GetMatchDB(string source)
        {

            switch (source.ToLower())
            {
                case "release":
                    return new MatchDB();
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
