using System;
using System.Collections.Generic;
using System.Text;
using Interface.Tournament;
using Model;

namespace DAL.Tournament
{
    public class TournamentDB : ITournamentDB
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
                new string[] {"@DateTime", tournamentDTO.StartTime.ToString("yyyy-MM-dd hh:mm:ss") },
                new string[] {"@Status", ((int)tournamentDTO.Status).ToString()},
                new string[] {"@TeamSize", tournamentDTO.TeamSize.ToString()},
            };
            return SQLConnection.ExecuteNonSearchQueryParameters($"INSERT INTO Tournament (ID,Name,OrganisationID,Size,Prize,BuyIn,Game,StartTime,Status,TeamSize) VALUES (@ID, @Name,@OrganisationID,@Size,@Prize,@BuyIn,@Game,@DateTime,@Status,@TeamSize);", param);
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
                new string[] {"@Game", ((int)tournamentDTO.Game).ToString() },
                new string[] {"@StartTime", tournamentDTO.StartTime.ToString("YYYY-MM-DD hh:mm:ss") },
                new string[] {"@TeamSize", tournamentDTO.TeamSize.ToString()},
            };
            return SQLConnection.ExecuteNonSearchQueryParameters($"UPDATE Tournament SET `Name` = @Name, `OrganisationID` = @OrganisationID, `Size` = @Size, `Prize` = @Prize, `BuyIn` = @BuyIn, `Game` = @Game, `TeamSize` = @TeamSize WHERE ID= @ID", param);
        }
        public TournamentDTO FindTournamentByName(string Name)
        {
            List<string[]> param = new List<string[]>()
            {
                new string[] {"@Name", Name},
            };
            var result = SQLConnection.ExecuteSearchQueryParameters($"SELECT * FROM Tournament WHERE `Name` = @Name", param);
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
        public List<TournamentDTO> FindAllFutureTournaments()
        {
            var now = DateTime.Now.ToString("YYYY-MM-DD hh:mm:ss");
            var result = SQLConnection.ExecuteSearchQuery($"SELECT * FROM Tournament WHERE Status = 0 ORDER BY StartTime ASC;");
            return GenerateDTOsFromRows(result);
        }

        public bool AddUserToTournament(string UserID, string TournamentID)
        {
            List<string[]> param = new List<string[]>()
            {
                new string[] {"@UserID", UserID},
                new string[] {"@TournamentID", TournamentID},
            };
            return SQLConnection.ExecuteNonSearchQueryParameters("INSERT INTO UserTournament (UserID,TournamentID) VALUES (@UserID,@TournamentID)", param);
        }
        public bool AddTeamToTournament(string TeamID, string TournamentID)
        {
            List<string[]> param = new List<string[]>()
            {
                new string[] {"@TeamID", TeamID},
                new string[] {"@TournamentID", TournamentID},
            };
            return SQLConnection.ExecuteNonSearchQueryParameters("INSERT INTO TeamTournament (TeamID,TournamentID) VALUES (@TeamID,@TournamentID)", param);
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
                Game = (Games)Convert.ToInt32(row[6]),
                StartTime = DateTime.Parse(row[7]),
                Status = (TourneyStatus)Convert.ToInt32(row[8]),
                TeamSize = Convert.ToInt32(row[9])
            };
            return tournamentDTO;
        }

        private List<TournamentDTO> GenerateDTOsFromRows(List<string[]> rows)
        {
            List<TournamentDTO> tournamentList = new List<TournamentDTO>();
            foreach (string[] row in rows)
            {
                tournamentList.Add(new TournamentDTO()
                {
                    ID = row[0],
                    Name = row[1],
                    OrganisationID = row[2],
                    Size = Convert.ToInt32(row[3]),
                    Prize = Convert.ToInt32(row[4]),
                    BuyIn = Convert.ToInt32(row[5]),
                    Game = (Games)Convert.ToInt32(row[6]),
                    StartTime = DateTime.Parse(row[7]),
                    Status = (TourneyStatus)Convert.ToInt32(row[8]),
                    TeamSize = Convert.ToInt32(row[9])
                });
            }
            return tournamentList;
        }
    }
}
