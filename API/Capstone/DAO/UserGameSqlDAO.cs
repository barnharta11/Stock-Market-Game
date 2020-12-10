using Capstone.Models;
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

        public int InviteUser(Invite inviteRequest)
        {
            int userGameId;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand command = new SqlCommand("INSERT INTO user_games ( user_id, game_id, status_code) VALUES (@user_id, @game_id, @status_code); INSERT INTO portfolio (user_game_id ,balance) VALUEs (@@IDENTITY ,100000);", conn);
                    command.Parameters.AddWithValue("@user_id", inviteRequest.UserId);
                    command.Parameters.AddWithValue("@game_id", inviteRequest.GameId);
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
