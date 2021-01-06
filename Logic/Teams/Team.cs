using System;
using System.Collections.Generic;
using System.Text;
using DAL;
using Interface;
using Model;

namespace Logic.Teams
{
    public class Team
    {
        ITeamDB teamDB = new TeamDB();
        EmailManager emailManager = new EmailManager();

        public string TeamID { get; set; }
        public string TeamName { get; set; }
        public int MinimumElo { get; set; }
        public bool IsPrivate { get; set; }
        public string Description { get; set; }
        public Countries Country { get; set; }
        public Languages Language { get; set; }
        public int MinimumAge { get; set; }
        public Games PlayedGame { get; set; }

        public Team(TeamDTO teamDTO)
        {
            TeamID = teamDTO.TeamID;
            TeamName = teamDTO.TeamName;
            MinimumAge = teamDTO.MinimumAge;
            MinimumElo = teamDTO.MinimumElo;
            IsPrivate = teamDTO.IsPrivate;
            Description = teamDTO.Description;
            Country = teamDTO.Country;
            Language = teamDTO.Language;
            PlayedGame = teamDTO.PlayedGame;
        }

        public Team()
        {

        }

        public List<TeamMatchResultDTO> GetResults() => teamDB.GetPreviousResults(this.TeamID);

        public bool UpdateTeam()
        {
            TeamDTO teamDTO = new TeamDTO()
            {
                TeamID = this.TeamID,
                TeamName = this.TeamName,
                MinimumAge = this.MinimumAge,
                MinimumElo = this.MinimumElo,
                IsPrivate = this.IsPrivate,
                Description = this.Description,
                Country = this.Country,
                Language = this.Language,
                PlayedGame = this.PlayedGame
            };
            return teamDB.EditTeam(teamDTO);
        }

        public bool AddPlayer(string UserId,TeamRoles role)
        {
            return teamDB.AddPlayerToTeam(UserId, this.TeamID, (int)role);
        }

        public List<string> GetMembers() => teamDB.GetMembers(TeamID);
        

        public void RecieveInvite()
        {
            throw new NotImplementedException();
        }

        public void SendInvite()
        {
            emailManager.SendInvite("tijnvanveghel@gmail.com", "Tijn van Veghel", "F4DED");
        }

        public Stats GetStats()
        {
            return new Stats { Wins = 2, Losses = 1 };
        }

        public TeamRoles GetRole(string UserID) => teamDB.GetRole(UserID,TeamID);

        public struct Stats
        {
            public int Wins;
            public int Losses;
        }
    }
}
