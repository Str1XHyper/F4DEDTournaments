using Model;
using System.Collections.Generic;

namespace Interface.Ladder
{
    public interface ILadderManagerDB
    {
        bool CreateLadder(LadderDTO ladderDTO);
        LadderDTO FindLadderByID(string ID);
        List<LadderDTO> GetAllLadders();
        List<LadderDTO> GetCompatibleLadder(int Elo);
    }
}