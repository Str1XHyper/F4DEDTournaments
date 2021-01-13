using DalFactory;
using IdGenerator;
using Interface.Ladder;
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
        ILadderManagerDB ladderDB = LadderFactory.GetLadderManagerDB("release");
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
