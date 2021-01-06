using DAL.Tournament;
using Interface.Tournament;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Tournament
{
    public class Tournament
    {
        private ITournamentDB tournament = new TournamentDB();
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

        public string[] GetUsers() => tournament.GetUsers(ID);
    }
}
