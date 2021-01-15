using Interface.Ladder;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestDAL.Ladder
{
    public class TestLadderDB : ILadderManagerDB
    {
        public bool CreateLadder(LadderDTO ladderDTO)
        {
            return true;
        }

        public LadderDTO FindLadderByID(string ID)
        {
            throw new NotImplementedException();
        }

        public List<LadderDTO> GetAllLadders()
        {
            throw new NotImplementedException();
        }

        public List<LadderDTO> GetCompatibleLadder(int Elo)
        {
            throw new NotImplementedException();
        }
    }
}
