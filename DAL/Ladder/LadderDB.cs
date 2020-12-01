using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Ladder
{
    public class LadderDB
    {
        public bool CreateLadder(LadderDTO ladderDTO)
        {
            List<string[]> param = new List<string[]>()
            {
                new string[] { "@ID", ladderDTO.ID},
                new string[] { "@Name", ladderDTO.Name},
                new string[] { "@MinimumElo", ladderDTO.MinimumElo.ToString()},
                new string[] { "@MaximumElo", ladderDTO.MaximumElo.ToString()},
                new string[] { "@Game", ((int)ladderDTO.Game).ToString()},
            };
            var result = SQLConnection.ExecuteNonSearchQueryParameters($"INSERT INTO Ladder (ID,Name,MinimumElo,MaximumElo,Game) VALUES (@ID, @Name,@MinimumElo,@MaximumElo,@Game);", param);
            return result;
        }

        public bool EditLadder(LadderDTO ladderDTO)
        {
            List<string[]> param = new List<string[]>()
            {
                new string[] { "@ID", ladderDTO.ID},
                new string[] { "@Name", ladderDTO.Name},
                new string[] { "@MinimumElo", ladderDTO.MinimumElo.ToString()},
                new string[] { "@MaximumElo", ladderDTO.MaximumElo.ToString()},
                new string[] { "@Game", ((int)ladderDTO.Game).ToString()},
            };
            var result = SQLConnection.ExecuteNonSearchQueryParameters($"Update Ladder SET 'Name' = @Name, 'MinimumElo' = @MinimumElo, 'MaximumElo' = @MaximumElo, 'Game' = @Game WHERE ID= @ID;", param);
            return result;
        }
        public LadderDTO FindLadderByID(string ID)
        {
            List<string[]> param = new List<string[]>()
            {
                new string[] {"@ID", ID}
            };
            var result = SQLConnection.ExecuteSearchQueryParameters("SELECT * FROM Ladder WHERE ID = @ID", param);
            return GenerateDTOFromRow(result[0]);
        }

        public List<LadderDTO> GetAllLadders()
        {
            var result = SQLConnection.ExecuteSearchQuery("SELECT * FROM Ladder");
            return GenerateDTOsFromRows(result);
        }

        public List<LadderDTO> GetCompatibleLadder(int Elo)
        {
            List<string[]> param = new List<string[]>()
            {
                new string[] {"@Elo", Elo.ToString()},
            };
            var result = SQLConnection.ExecuteSearchQueryParameters("SELECT * FROM Ladder WHERE MinimumElo < @Elo, MaximumElo > @Elo", param);
            return GenerateDTOsFromRows(result);
        }
        private LadderDTO GenerateDTOFromRow(string[] row)
        {

            LadderDTO ladderDTO = new LadderDTO()
            {
                ID = row[0],
                Name = row[1],
                MinimumElo = Convert.ToInt32(row[2]),
                MaximumElo = Convert.ToInt32(row[3]),
                Game = (Games)Convert.ToInt32(row[4])
            };
            return ladderDTO;
        }

        private List<LadderDTO> GenerateDTOsFromRows(List<string[]> rows)
        {
            List<LadderDTO> ladderList = new List<LadderDTO>();
            foreach (string[] row in rows)
            {
                ladderList.Add(new LadderDTO()
                {
                    ID = row[0],
                    Name = row[1],
                    MinimumElo = Convert.ToInt32(row[2]),
                    MaximumElo = Convert.ToInt32(row[3]),
                    Game = (Games)Convert.ToInt32(row[4])
                });
            }
            return ladderList;
        }
    }
}
