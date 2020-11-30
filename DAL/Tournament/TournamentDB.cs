﻿using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace DAL.Tournament
{
    public class TournamentDB
    {
        public bool CreateTournament(TournamentDTO tournamentDTO)
        {
            List<string[]> param = new List<string[]>()
            {
                new string[] {"@ID", tournamentDTO.ID},
                new string[] {"@Name", tournamentDTO.Name},
                new string[] {"@OrganisationID", tournamentDTO.OrganisationID},
                new string[] {"@Size", tournamentDTO.Size.ToString()},
                new string[] {"@Prize", tournamentDTO.Prize.ToString()},
                new string[] {"@BuyIn", tournamentDTO.BuyIn.ToString()},
                new string[] {"@Game", ((int)tournamentDTO.Game).ToString()},
            };
            return SQLConnection.ExecuteNonSearchQueryParameters($"INSERT INTO Tournament (ID,Name,OrganisationID,Size,Prize,BuyIn,Game) VALUES (@ID, @Name,@OrganisationID,@Size,@Prize,@BuyIn,@Game);", param);
        }
        
        public bool EditTournament(TournamentDTO tournamentDTO)
        {
            List<string[]> param = new List<string[]>()
            {
                new string[] {"@ID", tournamentDTO.ID},
                new string[] {"@Name", tournamentDTO.Name},
                new string[] {"@OrganisationID", tournamentDTO.OrganisationID},
                new string[] {"@Size", tournamentDTO.Size.ToString()},
                new string[] {"@Prize", tournamentDTO.Prize.ToString()},
                new string[] {"@BuyIn", tournamentDTO.BuyIn.ToString()},
                new string[] {"@Game", ((int)tournamentDTO.Game).ToString()},
            };
            return SQLConnection.ExecuteNonSearchQueryParameters($"UPDATE Tournament SET `Name` = @Name, `OrganisationID` = @OrganisationID, `Size` = @Size, `Prize` = @Prize, `BuyIn` = @BuyIn, `Game` = @Game WHERE ID= @ID",param);
        }
        public TournamentDTO FindTournamentByName(string Name)
        {
            List<string[]> param = new List<string[]>()
            {
                new string[] {"@Name", Name},
            };
            var result = SQLConnection.ExecuteSearchQueryParameters($"SELECT * FROM Tournament WHERE `Name` = @Name",param);
            if (result.Count == 0)
            {
                return null;
            }
            return GenerateDTOFromRow(result[0]);
        }

        public TournamentDTO FindTournamentByID(string ID)
        {
            List<string[]> param = new List<string[]>()
            {
                new string[] {"@ID", ID},
            };
            var result = SQLConnection.ExecuteSearchQueryParameters($"SELECT * FROM Tournament Where ID = @ID'", param);
            return GenerateDTOFromRow(result[0]);
        }
        public List<TournamentDTO> FindAllTournaments()
        {
            var result = SQLConnection.ExecuteSearchQuery($"SELECT * FROM Tournament");
            return GenerateDTOsFromRows(result);
        }

        public bool AddUserToLadder(string UserID, string LadderID)
        {
            List<string[]> param = new List<string[]>()
            {
                new string[] {"@UserID", UserID},
                new string[] {"@LadderID", LadderID},
            };
            return SQLConnection.ExecuteNonSearchQueryParameters("INSERT INTO UserLadder (UserID,LadderID) VALUES (@UserID,@LadderID)", param);
        }

        private TournamentDTO GenerateDTOFromRow(string[] row)
        {

            TournamentDTO tournamentDTO = new TournamentDTO()
            {
                ID = row[0],
                Name = row[1],
                OrganisationID = row[2],
                Size = Convert.ToInt32(row[3]),
                Prize = Convert.ToInt32(row[4]),
                BuyIn = Convert.ToInt32(row[5]),
                Game = (Games)Convert.ToInt32(row[6])
            };
            return tournamentDTO;
        }

        private List<TournamentDTO> GenerateDTOsFromRows(List<string[]> rows)
        {
            List<TournamentDTO> tournamentDTO = new List<TournamentDTO>();
            foreach (string[] row in rows)
            {
                tournamentDTO.Add(new TournamentDTO()
                {
                    ID = row[0],
                    Name = row[1],
                    OrganisationID = row[2],
                    Size = Convert.ToInt32(row[3]),
                    Prize = Convert.ToInt32(row[4]),
                    BuyIn = Convert.ToInt32(row[5]),
                    Game = (Games)Convert.ToInt32(row[6])
                });
            }
            return tournamentDTO;
        }
    }
}
