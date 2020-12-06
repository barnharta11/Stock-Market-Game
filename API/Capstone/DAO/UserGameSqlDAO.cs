using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO
{
    public class UserGameSqlDAO : IUserGameDAO
    {
        private readonly string connectionString;

        public UserGameSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }
        public void AcceptInvite(int userGameId)
        {
            throw new NotImplementedException();
        }

        public int InviteUser(int userId, int gameId)
        {
            int userGameId;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand command = new SqlCommand("INSERT INTO user_games ( user_id, game_id, balance, status_code) VALUES (@user_id, @game_id, @balance, @status_code)", conn);
                    command.Parameters.AddWithValue("@user_id", userId);
                    command.Parameters.AddWithValue("@game_id", gameId);
                    command.Parameters.AddWithValue("@balance", 100000);
                    command.Parameters.AddWithValue("@status_code", 0);//0 is pending
                    userGameId = Convert.ToInt32(command.ExecuteScalar());

                }
            }
            catch (SqlException)
            {
                throw;
            }

            return userGameId;
        }
    }
}
