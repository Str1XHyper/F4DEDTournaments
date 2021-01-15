using Interface.Match;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestDAL.Match
{
    public class TestMatchDB : IMatchManagerDB
    {
        public bool CreateMatch(MatchDTO matchDTO)
        {
            return true;
        }
    }
}
