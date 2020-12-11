using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO
{
    public class AssetSqlDAO : IAssetsDAO
    {

        private readonly string connectionString;

        public AssetSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public List<Asset> GetAssets(int userID, int gameID)
        {
            List<Asset> returnAssets = new List<Asset>();
            Asset returnAsset = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("select * from portfolio join portfolio_assets on portfolio.portfolio_id = portfolio_assets.portfolio_id join assets on portfolio_assets.asset_id = assets.asset_id join user_games on portfolio.user_game_id=user_games.user_game_id Where user_id=@userID and game_id=@gameID", conn);
                    cmd.Parameters.AddWithValue("@userID", userID);
                    cmd.Parameters.AddWithValue("@gameID", gameID);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows && reader.Read())
                    {
                        while (reader.Read())
                        {
                            returnAsset = GetAssetFromReader(reader);
                            returnAssets.Add(returnAsset);
                        }
                        
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return returnAssets;
        }

     
        private Asset GetAssetFromReader(SqlDataReader reader)
        {
            Asset a = new Asset()
            {
             
                QuantityHeld = Convert.ToInt32(reader["quantity_held"]),
                Symbol = Convert.ToString(reader["stock_ticker"]),
                CompanyName = Convert.ToString(reader["company_name"]),
                CurrentPrice = Convert.ToDecimal(reader["current_price"]),
              
            };

            return a;
        }


        //public List<ReturnPortfolio> ListAllPortfolios()
        //{
        //    List<ReturnPortfolio> returnPortfolioList = new List<ReturnPortfolio>();
        //    try
        //    {
        //        using (SqlConnection conn = new SqlConnection(connectionString))
        //        {
        //            conn.Open();
        //            SqlCommand cmd = new SqlCommand("Select * from portfolio", conn);
        //            SqlDataReader reader = cmd.ExecuteReader();

        //            while (reader.Read())
        //            {
        //                ReturnPortfolio portfolio = new ReturnPortfolio();
        //                portfolio.Balance = Convert.ToDecimal(reader["balance"]);
        //                portfolio.QuantityHeld = Convert.ToInt32(reader["quantity_held"]);
        //                portfolio.StockTicker = Convert.ToString(reader["stock_ticker"]);
        //                portfolio.CompanyName = Convert.ToString(reader["company_name"]);
        //                portfolio.CurrentPrice = Convert.ToDecimal(reader["current_price"]);
        //                portfolio.CostBasis = Convert.ToDecimal(reader["cost_basis"]);
        //                portfolio.Time = Convert.ToDateTime(reader["time"]);
        //                returnPortfolioList.Add(portfolio);

        //            }

        //        }
        //    }
        //    catch (SqlException)
        //    {

        //        throw;

        //    };

        //    return returnPortfolioList;
        //}
    }
}



//Select balance, quantity_held, stock_ticker, company_name, current_price, cost_basis, time from portfolio left join portfolio_stocks on portfolio.portfolio_id = portfolio_stocks.portfolio_id left join stocks on portfolio_stocks.stock_id = stocks.stock_id left join transactions on stocks.stock_id = transactions.stock_id  left join user_games on portfolio.user_game_id=user_games.user_game_id Where user_id=@userID and game_id=@gameID", conn);


