using DAL.Ladder;
using Interface.Ladder;
using System;
using System.Collections.Generic;
using System.Text;

namespace DalFactory
{
    public static class LadderFactory
    {
        public static ILadderDB GetLadderDB(string source)
        {
            switch (source.ToLower())
            {
                case "release":
                    return new LadderDB();
                default:
                    throw new NotImplementedException();
            }
        }

        public static ILadderManagerDB GetLadderManagerDB(string source)
        {
            switch (source.ToLower())
            {
                case "release":
                    return new LadderDB();
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
