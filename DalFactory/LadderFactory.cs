using DAL.Ladder;
using Interface.Ladder;
using System;
using System.Collections.Generic;
using System.Text;
using TestDAL.Ladder;

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
                case "test":
                    return new TestLadderDB();
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
