using Model;

namespace DAL
{
    public interface ITeamDB
    {
        bool AddPlayerToTeam(string PlayerID, string TeamID, int Role);
        bool CreateTeam(TeamDTO teamDTO);
        bool EditTeam(TeamDTO teamDTO);
        TeamDTO FindTeamByID(string ID);
        TeamDTO FindTeamByName(string Name);
    }
}