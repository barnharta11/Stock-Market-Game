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
        public void AcceptInvite(Invite invite)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand("update user_games set user_games.status_code = 1 where user_games.game_id=@gameid and user_games.user_id=@userid", conn);
                    command.Parameters.AddWithValue("@userid", invite.UserId);
                    command.Parameters.AddWithValue("@gameid", invite.GameId);
                }
            }
            catch (SqlException)
            {
                throw;
            }
        }

        public int InviteUser(Invite inviteRequest)
        {
            int userGameId;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand command = new SqlCommand("INSERT INTO user_games ( user_id, game_id, status_code) VALUES (@user_id, @game_id, @status_code); INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY); INSERT INTO portfolio_assets(portfolio_id, asset_id, quantity_held) Values (@@IDENTITY, 1, 100000)", conn);
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
