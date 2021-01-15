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
        MinHigherThanMax = 1,
    }
    public class LadderManager
    {
        ILadderManagerDB ladderDB = LadderFactory.GetLadderManagerDB("release");
        Generator generator = new Generator();

        public LadderManager(string source)
        {
            ladderDB = LadderFactory.GetLadderManagerDB(source);
        }

        public LadderManager()
        {

        }

        public LadderErrorCodes CreateLadder(LadderDTO ladderDTO)
        {
            if(ladderDTO.MinimumElo >= ladderDTO.MaximumElo)
            {
                return LadderErrorCodes.MinHigherThanMax;
            }
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
