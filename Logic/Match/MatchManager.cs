using DAL.Match;
using IdGenerator;
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
        MatchDB matchDB = new MatchDB();
        Generator generator = new Generator();

        public MatchErrorCodes CreateMatch(MatchDTO matchDTO)
        {
            matchDTO.ID = generator.GenerateID(12);
            var result = matchDB.CreateMatch(matchDTO);
            if(result == false)
            {
                return MatchErrorCodes.UnknownException;
            }
            return MatchErrorCodes.NoError;
        }
    }
}
