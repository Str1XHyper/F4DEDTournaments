using DAL;
using DAL.Tournament;
using DAL.User;
using Interface;
using Interface.Tournament;
using Interface.User;
using System;
using System.Collections.Generic;
using System.Text;
using TestDAL.Team;

namespace Logic
{
    public class Factory
    {
        public ITeamDB GetTeamDB(string source)
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

        public ITeamCollectionDB GetTeamManagerDB(string source)
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

        public ITournamentDB GetTournamentDB(string source)
        {
            switch (source.ToLower())
            {
                case "release":
                    return new TournamentDB();
                default:
                    throw new NotImplementedException();
            }
        }

        public ITournamentManagerDB GetTournamentManagerDB(string source)
        {
            switch (source.ToLower())
            {
                case "release":
                    return new TournamentDB();
                default:
                    throw new NotImplementedException();
            }
        }

        public IUserDB GetUserDB(string source)
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
