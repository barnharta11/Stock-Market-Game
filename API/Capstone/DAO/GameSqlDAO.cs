using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO
{
    public class GameSqlDAO : IGameDAO
    {
        private readonly string connectionString;

        public GameSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }
        public Game AddGame(string gameName, int creatorId, DateTime startDate, DateTime endDate)
        {

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand command = new SqlCommand("INSERT INTO games ( game_name, creator_id, start_date, end_date) VALUES (@gamename, @creatorid, @startdate, @enddate)", conn);
                    command.Parameters.AddWithValue("@gamename", gameName);
                    command.Parameters.AddWithValue("@creatorid", creatorId);
                    command.Parameters.AddWithValue("@startdate", startDate);
                    command.Parameters.AddWithValue("@enddate", endDate);
                    command.ExecuteNonQuery();

                }
            }
            catch (SqlException)
            {
                throw;
            }

            return GetGame(gameName);
        }

        public Game GetGame(string gameName)
        {

            Game returnGame = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM games WHERE game_name = @gamename", conn);
                    cmd.Parameters.AddWithValue("@gamename", gameName);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows && reader.Read())
                    {
                        returnGame = GetGameFromReader(reader);
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return returnGame;
        }

        public List<Game> GetGameList()
        {
            List<Game> returnList = new List<Game>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM games", conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Game readGame = new Game();
                        readGame.GameId = Convert.ToInt32(reader["game_id"]);
                        readGame.GameName = Convert.ToString(reader["game_name"]);
                        readGame.StartDate = Convert.ToDateTime(reader["start_date"]);
                        readGame.EndDate = Convert.ToDateTime(reader["end_date"]);
                        readGame.CreatorId = Convert.ToInt32(reader["creator_id"]);
                        //readGame.WinnerId = Convert.ToInt32(reader["winner_id"]);                      
                        returnList.Add(readGame);
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return returnList;
        }

        private Game GetGameFromReader(SqlDataReader reader)
        {
            Game searchGame = new Game()
            {
                GameId = Convert.ToInt32(reader["game_id"]),
                GameName = Convert.ToString(reader["game_name"]),
                CreatorId = Convert.ToInt32(reader["creator_id"]),
                StartDate = Convert.ToDateTime(reader["start_date"]),
                EndDate = Convert.ToDateTime(reader["end_date"])

            };

            return searchGame;
        }
    }
}
