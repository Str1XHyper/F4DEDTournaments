using DAL.Ladder;
using IdGenerator;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Ladders
{
    public enum LadderErrorCodes
    {
        UnknownException = -1,
        NoError = 0,
    }
    public class LadderManager
    {
        LadderDB ladderDB = new LadderDB();
        Generator generator = new Generator();

        public LadderErrorCodes CreateLadder(LadderDTO ladderDTO)
        {
            ladderDTO.ID = generator.GenerateID(12);
            var result = ladderDB.CreateLadder(ladderDTO);
            if(result == false)
            {
                return LadderErrorCodes.UnknownException;
            }
            return LadderErrorCodes.NoError;
        }
    }
}
