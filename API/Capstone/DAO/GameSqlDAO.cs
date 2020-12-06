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
            int gameId = 0;
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
                    gameId = Convert.ToInt32(command.ExecuteScalar());

                }
            }
            catch (SqlException)
            {
                throw;
            }

            return GetGame(gameId);
        }

        public Game GetGame(int gameId)
        {

            Game returnGame = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM games WHERE game_id = @gameid", conn);
                    cmd.Parameters.AddWithValue("@gameid", gameId);
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
