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
            startDate.AddHours(9).AddMinutes(30);
            endDate.AddHours(16);
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand command = new SqlCommand("INSERT INTO games ( game_name, creator_id, start_date, end_date) VALUES (@gamename, @creatorid, @startdate, @enddate); INSERT INTO user_games ( user_id, game_id, status_code) VALUES (@creatorid, @@IDENTITY, @statuscode); INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY); INSERT INTO portfolio_assets(portfolio_id, asset_id, quantity_held) Values (@@IDENTITY, 1, 100000)", conn);
                    command.Parameters.AddWithValue("@gamename", gameName);
                    command.Parameters.AddWithValue("@creatorid", creatorId);
                    command.Parameters.AddWithValue("@startdate", startDate);
                    command.Parameters.AddWithValue("@enddate", endDate);
                    command.Parameters.AddWithValue("@statuscode", 1);
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

                    SqlCommand cmd = new SqlCommand("select games.game_id, games.game_name, users.username, games.start_date, games.end_date from games join users on creator_id = users.USER_ID", conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Game readGame = new Game();
                        readGame.GameId = Convert.ToInt32(reader["game_id"]);
                        readGame.GameName = Convert.ToString(reader["game_name"]);
                        readGame.StartDate = Convert.ToDateTime(reader["start_date"]);
                        readGame.EndDate = Convert.ToDateTime(reader["end_date"]);
                        //readGame.CreatorId = Convert.ToInt32(reader["creator_id"]);
                        //readGame.WinnerId = Convert.ToInt32(reader["winner_id"]);
                        readGame.CreatorName = Convert.ToString(reader["username"]);
                        returnList.Add(readGame);
                    }

                    foreach(Game game in returnList)
                    {
                        if (DateTime.Compare(game.StartDate, DateTime.Now) > 0)
                        {
                            SqlCommand command = new SqlCommand("update user_games set status_code = 2 where game_id = @gameid", conn);
                            command.Parameters.AddWithValue("@gameid", game.GameId);
                            command.ExecuteNonQuery();
                            game.StatusName = "Active";
                        }
                        if (DateTime.Compare(game.EndDate, DateTime.Now) > 0)
                        {
                            SqlCommand command = new SqlCommand("update user_games set status_code = 3 where game_id = @gameid", conn);
                            command.Parameters.AddWithValue("@gameid", game.GameId);
                            command.ExecuteNonQuery();
                            game.StatusName = "Completed";
                        }
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return returnList;
        }

        public List<Leaderboard> GetLeaderboard(int gameID)
        {
            List<Leaderboard> returnList = new List<Leaderboard>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("Select SUM(quantity_held * current_price) AS 'net_worth', users.user_id, users.username From portfolio left join portfolio_assets on portfolio.portfolio_id = portfolio_assets.portfolio_id left join assets on portfolio_assets.asset_id = assets.asset_id left join user_games on portfolio.user_game_id = user_games.user_game_id left join users on user_games.user_id = users.user_id where game_id = @gameid Group By users.user_id, users.username order by 'net_worth' desc", conn);
                    cmd.Parameters.AddWithValue("@gameid", gameID);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Leaderboard readLeaderboard = new Leaderboard();
                        //readLeaderboard.AssetsID = Convert.ToInt32(reader["asset_id"]);
                        //readLeaderboard.PortfolioID = Convert.ToInt32(reader["portfolio_id"]);
                        //readLeaderboard.UserGameID = Convert.ToInt32(reader["user_game_id"]);
                        //readLeaderboard.GameID = Convert.ToInt32(reader["game_id"]);
                        readLeaderboard.UserID = Convert.ToInt32(reader["user_id"]);
                        readLeaderboard.NetWorth = Convert.ToDecimal(reader["net_worth"]);
                        //readLeaderboard.QuantityHeld = Convert.ToDecimal(reader["quantity_held"]);
                        //readLeaderboard.CurrentPrice = Convert.ToDecimal(reader["current_price"]);
                        readLeaderboard.UserName = Convert.ToString(reader["username"]);
                        returnList.Add(readLeaderboard);
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return returnList;
        }

        public List<Game> GetGamesByUser(int userID)
        {
            List<Game> returnList = new List<Game>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand gamecmd = new SqlCommand("select games.game_id, games.game_name, users.username, games.start_date, games.end_date, game_status.status_name from user_games join games on user_games.game_id = games.game_id  join users as u on user_games.user_id = u.user_id join users on games.creator_id = users.user_id join game_status on user_games.status_code = game_status.status_id where user_games.user_id = @userId order by user_games.status_code", conn);
                    gamecmd.Parameters.AddWithValue("@userId", userID);
                    SqlDataReader gameReader = gamecmd.ExecuteReader();


                    while (gameReader.Read())
                    {
                        Game readGame = new Game();
                        readGame.GameId = Convert.ToInt32(gameReader["game_id"]);
                        readGame.GameName = Convert.ToString(gameReader["game_name"]);
                        readGame.StartDate = Convert.ToDateTime(gameReader["start_date"]);
                        readGame.EndDate = Convert.ToDateTime(gameReader["end_date"]);
                        readGame.CreatorName = Convert.ToString(gameReader["username"]);
                        readGame.StatusName = Convert.ToString(gameReader["status_name"]);
                        returnList.Add(readGame);
                    }
                    foreach (Game game in returnList)
                    {
                        if (DateTime.Compare(game.StartDate, DateTime.Now) > 0)
                        {
                            SqlCommand command = new SqlCommand("update user_games set status_code = 2 where game_id = @gameid", conn);
                            command.Parameters.AddWithValue("@gameid", game.GameId);
                            command.ExecuteNonQuery();
                            game.StatusName = "Active";
                        }
                        if (DateTime.Compare(game.EndDate, DateTime.Now) > 0)
                        {
                            SqlCommand command = new SqlCommand("update user_games set status_code = 3 where game_id = @gameid", conn);
                            command.Parameters.AddWithValue("@gameid", game.GameId);
                            command.ExecuteNonQuery();
                            game.StatusName = "Completed";
                        }
                    }
                    gameReader.Close();

                    foreach (Game element in returnList)
                    {
                        SqlCommand leadercmd = new SqlCommand("Select SUM(quantity_held * current_price) AS 'net_worth', users.user_id, users.username From portfolio left join portfolio_assets on portfolio.portfolio_id = portfolio_assets.portfolio_id left join assets on portfolio_assets.asset_id = assets.asset_id left join user_games on portfolio.user_game_id = user_games.user_game_id left join users on user_games.user_id = users.user_id where game_id = @gameid Group By users.user_id, users.username order by 'net_worth' desc", conn);
                        leadercmd.Parameters.AddWithValue("@gameid", element.GameId);
                        SqlDataReader leaderReader = leadercmd.ExecuteReader();
                        List<Leaderboard> readLeaderboard = new List<Leaderboard>();
                        while (leaderReader.Read())
                        {
                            Leaderboard leaderBoard = new Leaderboard();
                            //readLeaderboard.AssetsID = Convert.ToInt32(reader["asset_id"]);
                            //readLeaderboard.PortfolioID = Convert.ToInt32(reader["portfolio_id"]);
                            //readLeaderboard.UserGameID = Convert.ToInt32(reader["user_game_id"]);
                            //readLeaderboard.GameID = Convert.ToInt32(reader["game_id"]);
                            leaderBoard.UserID = Convert.ToInt32(leaderReader["user_id"]);
                            leaderBoard.NetWorth = Convert.ToDecimal(leaderReader["net_worth"]);
                            //readLeaderboard.QuantityHeld = Convert.ToDecimal(reader["quantity_held"]);
                            //readLeaderboard.CurrentPrice = Convert.ToDecimal(reader["current_price"]);
                            leaderBoard.UserName = Convert.ToString(leaderReader["username"]);
                            readLeaderboard.Add(leaderBoard);
                            
                        }
                        element.LeaderboardList = readLeaderboard;
                        leaderReader.Close();
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
