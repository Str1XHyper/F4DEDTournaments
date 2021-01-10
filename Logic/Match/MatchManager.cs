using DAL.Match;
using IdGenerator;
using Interface.Match;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Match
{
    public enum MatchErrorCodes
    {
        UnknownException = -1,
        NoError = 0,
    }
    public class MatchManager
    {
        IMatchManagerDB matchDB = MatchFactory.GetMatchDB("release");
        Generator idGenerator = new Generator();

        public MatchErrorCodes CreateMatch(MatchDTO matchDTO)
        {
            matchDTO.ID = idGenerator.GenerateID(12);
            var result = matchDB.CreateMatch(matchDTO);
            if(result == false)
            {
                return MatchErrorCodes.UnknownException;
            }
            return MatchErrorCodes.NoError;
        }
    }
}
