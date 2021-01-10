using DAL.Tournament;
using Interface.Tournament;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Tournament
{
    public enum TournamentErrorCode
    {
        UnknownError = -1,
        NoError = 0,
        CouldntAddUserToTournament = 1,
        CouldntAddTeamToTournament = 2,
        CouldntUpdate = 3,
    }
    public class Tournament
    {
        private ITournamentDB tournamentDB = TournamentFactory.GetTournamentDB("release");
        public string ID { get; set; }
        public string Name { get; set; }
        public string OrganisationID { get; set; }
        public string UserID { get; set; }
        public int Size { get; set; }
        public int Prize { get; set; }
        public int BuyIn { get; set; }
        public Games Game { get; set; }
        public DateTime StartTime { get; set; }
        public TourneyStatus Status { get; set; }
        public int TeamSize { get; set; }

        public Tournament(TournamentDTO tournamentDTO)
        {
            ID = tournamentDTO.ID;
            Name = tournamentDTO.Name;
            OrganisationID = tournamentDTO.OrganisationID;
            UserID = tournamentDTO.UserID;
            Size = tournamentDTO.Size;
            Prize = tournamentDTO.Prize;
            BuyIn = tournamentDTO.BuyIn;
            Game = tournamentDTO.Game;
            StartTime = tournamentDTO.StartTime;
            Status = tournamentDTO.Status;
            TeamSize = tournamentDTO.TeamSize;
        }
        public Tournament()
        {
        }

        public string[] GetUsers() => tournamentDB.GetUsers(ID);

        public TournamentErrorCode AddUser(string UserId)
        {
            var result = tournamentDB.AddUserToTournament(UserId, this.ID);
            if (result)
            {
                return TournamentErrorCode.NoError;
            }
            else
            {
                return TournamentErrorCode.CouldntAddUserToTournament;
            }
        }

        public TournamentErrorCode AddTeam(string teamID)
        {
            var result = tournamentDB.AddTeamToTournament(teamID, this.ID);
            if (result)
            {
                return TournamentErrorCode.NoError;
            }
            else
            {
                return TournamentErrorCode.CouldntAddTeamToTournament;
            }
        }

        private TournamentDTO CreateDTO()
        {
            return new TournamentDTO()
            {
                ID = this.ID,
                Name = this.Name,
                OrganisationID = this.OrganisationID,
                UserID = this.UserID,
                Size = this.Size,
                Prize = this.Prize,
                BuyIn = this.BuyIn,
                Game = this.Game,
                StartTime = this.StartTime,
                Status = this.Status,
                TeamSize = this.TeamSize,
            };
        }

        public TournamentErrorCode Start()
        {
            Status = TourneyStatus.Active;
            var result = tournamentDB.EditTournament(CreateDTO());
            if (result)
            {
                return TournamentErrorCode.NoError;
            }
            return TournamentErrorCode.CouldntUpdate;
        }

        public TournamentErrorCode End()
        {
            Status = TourneyStatus.Finished;
            var result = tournamentDB.EditTournament(CreateDTO());
            if (result)
            {
                return TournamentErrorCode.NoError;
            }
            return TournamentErrorCode.CouldntUpdate;
        }
    }
}
