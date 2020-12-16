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
            //Creates game, then adds creator to user_games
            startDate.AddHours(9).AddMinutes(30);
            endDate.AddHours(16);
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand command = new SqlCommand("INSERT INTO games (game_name, creator_id, start_date, end_date) VALUES (@gamename, @creatorid, @startdate, @enddate); INSERT INTO user_games ( user_id, game_id, player_status_code) VALUES (@creatorid, @@IDENTITY, @playerstatuscode); INSERT INTO portfolio (user_game_id) VALUEs (@@IDENTITY); INSERT INTO portfolio_assets(portfolio_id, asset_id, quantity_held) Values (@@IDENTITY, 1, 100000)", conn);
                    command.Parameters.AddWithValue("@gamename", gameName);
                    command.Parameters.AddWithValue("@creatorid", creatorId);
                    command.Parameters.AddWithValue("@startdate", startDate);
                    command.Parameters.AddWithValue("@enddate", endDate);
                    command.Parameters.AddWithValue("@playerstatuscode", 0);
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
            //Checks to see if name is already in use
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
            //retrieves list of all games, each game's leaderboard, and each leaderboard entry's assets
            List<Game> returnList = new List<Game>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    //gets data about all games
                    SqlCommand cmd = new SqlCommand("select games.game_id, games.game_name, users.username, games.start_date, games.end_date, game_status.game_status_name from games join users on creator_id = users.USER_ID join game_status on games.game_status_code=game_status.game_status_id", conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Game readGame = new Game();
                        readGame.GameId = Convert.ToInt32(reader["game_id"]);
                        readGame.GameName = Convert.ToString(reader["game_name"]);
                        readGame.StartDate = Convert.ToDateTime(reader["start_date"]);
                        readGame.EndDate = Convert.ToDateTime(reader["end_date"]);
                        readGame.CreatorName = Convert.ToString(reader["username"]);
                        readGame.StatusName = Convert.ToString(reader["game_status_name"]);
                        //gets leaderboard for each game
                        readGame.LeaderboardList = GetLeaderboard(readGame.GameId);
                        returnList.Add(readGame);
                       
                    }
                    reader.Close();
                    foreach (Game game in returnList)
                    {
                        
                        //Updates game status
                        if (DateTime.Compare(game.StartDate, DateTime.Now) < 0)
                        {
                            SqlCommand command = new SqlCommand("update games set game_status_code = 1 where game_id = @gameid", conn);
                            command.Parameters.AddWithValue("@gameid", game.GameId);
                            command.ExecuteNonQuery();
                            game.StatusName = "Active";
                        }
                        if (DateTime.Compare(game.EndDate, DateTime.Now) < 0 && game.StatusName != "Completed")
                        {
                            SqlCommand command = new SqlCommand("update games set game_status_code = 2 where game_id = @gameid", conn);
                            command.Parameters.AddWithValue("@gameid", game.GameId);
                            command.ExecuteNonQuery();
                            game.StatusName = "Completed";
                            //Updates final networth
                            foreach (Leaderboard board in game.LeaderboardList)
                            {
                                UpdateFinalNetworth(board);
                            }

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
           //Makes the leaderboard entry list for each game
            List<Leaderboard> returnList = new List<Leaderboard>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("Select SUM(quantity_held * current_price) AS 'net_worth', player_status_name, users.user_id, users.username, final_networth From portfolio  join portfolio_assets on portfolio.portfolio_id = portfolio_assets.portfolio_id join assets on portfolio_assets.asset_id = assets.asset_id join user_games on portfolio.user_game_id = user_games.user_game_id join users on user_games.user_id = users.user_id join player_status on user_games.player_status_code=player_status.player_status_id where game_id = @gameid Group By users.user_id, users.username, final_networth, player_status_name order by 'net_worth' desc", conn);
                    cmd.Parameters.AddWithValue("@gameid", gameID);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Leaderboard readLeaderboard = new Leaderboard();
                        readLeaderboard.FinalNetworth = Convert.ToDecimal(reader["final_networth"]);
                        readLeaderboard.UserID = Convert.ToInt32(reader["user_id"]);
                        readLeaderboard.NetWorth = Convert.ToDecimal(reader["net_worth"]);
                        readLeaderboard.UserName = Convert.ToString(reader["username"]);
                        readLeaderboard.PlayerStatus = Convert.ToString(reader["player_status_name"]);
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

        public Leaderboard UpdateFinalNetworth(Leaderboard leaderboard)
        {
            decimal sumOfAssets = 0;
            foreach (Asset asset in leaderboard.PlayersAssets)
            {
                sumOfAssets += (asset.QuantityHeld * asset.CurrentPrice);

            }
            leaderboard.FinalNetworth = sumOfAssets;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand gamecmd = new SqlCommand("update portfolio_assets set final_networth = @finalnetworth where portfolio_id = @portfolioid", conn);
                gamecmd.Parameters.AddWithValue("@finalnetworth", leaderboard.FinalNetworth);
                gamecmd.Parameters.AddWithValue("@portfolioid", leaderboard.PortfolioID);
                SqlDataReader gameReader = gamecmd.ExecuteReader();
            }
            return leaderboard;
        }

        public List<Game> GetGamesByUser(int userID)
        {
            List<Game> returnList = new List<Game>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand gamecmd = new SqlCommand("select games.game_id, games.game_name, users.username, games.start_date, games.end_date, game_status.game_status_name from user_games join games on user_games.game_id = games.game_id join users as u on user_games.user_id = u.user_id join users on games.creator_id = users.user_id join game_status on games.game_status_code = game_status.game_status_id where user_games.user_id = @userId order by games.game_status_code", conn);
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
                        readGame.StatusName = Convert.ToString(gameReader["game_status_name"]);
                        //gets leaderboard for each game
                        readGame.LeaderboardList = GetLeaderboard(readGame.GameId);
                        returnList.Add(readGame);

                    }
                    gameReader.Close();
                    foreach (Game game in returnList)
                    {

                        //Updates game status
                        if (DateTime.Compare(game.StartDate, DateTime.Now) < 0)
                        {
                            SqlCommand command = new SqlCommand("update games set game_status_code = 1 where game_id = @gameid", conn);
                            command.Parameters.AddWithValue("@gameid", game.GameId);
                            command.ExecuteNonQuery();
                            game.StatusName = "Active";
                        }
                        if (DateTime.Compare(game.EndDate, DateTime.Now) < 0 && game.StatusName != "Completed")
                        {
                            SqlCommand command = new SqlCommand("update games set game_status_code = 2 where game_id = @gameid", conn);
                            command.Parameters.AddWithValue("@gameid", game.GameId);
                            command.ExecuteNonQuery();
                            game.StatusName = "Completed";
                            //Updates final networth
                            foreach (Leaderboard board in game.LeaderboardList)
                            {
                                UpdateFinalNetworth(board);
                            }

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
