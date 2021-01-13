using DAL.Tournament;
using Interface.Tournament;
using System;
using System.Collections.Generic;
using System.Text;
using TestDAL.Tournament;

namespace DalFactory
{
    public static class TournamentFactory
    {
        public static ITournamentDB GetTournamentDB(string source)
        {
            switch (source.ToLower())
            {
                case "release":
                    return new TournamentDB();
                default:
                    throw new NotImplementedException();
            }
        }

        public static ITournamentManagerDB GetTournamentManagerDB(string source)
        {
            switch (source.ToLower())
            {
                case "release":
                    return new TournamentDB();
                case "test":
                    return new TestTournamentDB();
                default:
                    throw new NotImplementedException();
            }
        }

    }
}
