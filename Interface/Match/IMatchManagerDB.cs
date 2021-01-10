using Model;

namespace Interface.Match
{
    public interface IMatchManagerDB
    {
        bool CreateMatch(MatchDTO matchDTO);
    }
}