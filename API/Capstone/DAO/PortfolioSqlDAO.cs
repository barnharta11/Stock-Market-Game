using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO
{
    public class PortfolioSqlDAO : IPortfolioDAO
    {

        private readonly string connectionString;

        public PortfolioSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public Portfolio GetPortfolio(int userID, int gameID)
        {

            Portfolio returnPortfolio = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("Select balance, quantity_held, stock_ticker, company_name, current_price, cost_basis, time from portfolio left join portfolio_stocks on portfolio.portfolio_id = portfolio_stocks.portfolio_id left join stocks on portfolio_stocks.stock_id = stocks.stock_id left join transactions on stocks.stock_id = transactions.stock_id  left join user_games on portfolio.user_game_id=user_games.user_game_id Where user_id=@userID and game_id=@gameID", conn);
                    cmd.Parameters.AddWithValue("@userID", userID);
                    cmd.Parameters.AddWithValue("@gameID", gameID);
              
                    
                   SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows && reader.Read())
                    {
                        returnPortfolio = GetPortfolioFromReader(reader);
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return returnPortfolio;
        }

        private Portfolio GetPortfolioFromReader(SqlDataReader reader)
        {
            Portfolio portfolio = new Portfolio()
            {
                Balance = Convert.ToDecimal(reader["balance"]),
                QuantityHeld = Convert.ToInt32(reader["quantity_held"]),
                StockTicker = Convert.ToString(reader["stock_ticker"]),
                CompanyName = Convert.ToString(reader["company_name"]),
                CurrentPrice = Convert.ToDecimal(reader["current_price"]),
                CostBasis = Convert.ToDecimal(reader["cost_basis"]),
                Time = Convert.ToDateTime(reader["time"])


            };

            return portfolio;
        }


    }
}
