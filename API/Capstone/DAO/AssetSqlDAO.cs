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
            List<Asset> returnList = new List<Asset>();


            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("select * from portfolio join portfolio_assets on portfolio.portfolio_id = portfolio_assets.portfolio_id join assets on portfolio_assets.asset_id = assets.asset_id join user_games on portfolio.user_game_id=user_games.user_game_id Where user_id=@userID and game_id=@gameID", conn);
                    cmd.Parameters.AddWithValue("@userID", userID);
                    cmd.Parameters.AddWithValue("@gameID", gameID);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        if (reader.HasRows)
                        {

                            Asset returnAsset = new Asset();
                            returnAsset = GetAssetFromReader(reader);
                            returnList.Add(returnAsset);
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

        public void UpdateQuantity(UpdateAsset updateAsset)
        {
            
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("update portfolio_assets set quantity_held += @input where portfolio_id = @portfolio_id AND asset_id = @asset_id; update portfolio_assets set quantity_held += @adjustment where portfolio_id = @portfolioid AND asset_id = 1;", conn);
                cmd.Parameters.AddWithValue("@input", updateAsset.QuantityAdjustment);
                cmd.Parameters.AddWithValue("@portfolio_id", updateAsset.PortfolioID);
                cmd.Parameters.AddWithValue("@asset_id", updateAsset.AssetId);
                cmd.Parameters.AddWithValue("@adjustment", updateAsset.USDAdjustment);
                cmd.ExecuteScalar();



            }
                    
        }

        public void BuySellAsset(UpdateAsset newAsset)
        {
            
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("INSERT INTO portfolio_assets (portfolio_id, asset_id, quantity_held) VALUES (@portfolioID, @assetID, @quantityHeld); update portfolio_assets set quantity_held += @adjustment where portfolio_id = @portfolioid AND asset_id = 1;", conn);
                cmd.Parameters.AddWithValue("@portfolioID", newAsset.PortfolioID);
                cmd.Parameters.AddWithValue("@assetID", newAsset.AssetId);
                cmd.Parameters.AddWithValue("@quantityHeld", newAsset.QuantityAdjustment);
                cmd.Parameters.AddWithValue("@adjustment", newAsset.USDAdjustment);
                cmd.ExecuteScalar();



            }
           
        }


        private Asset GetAssetFromReader(SqlDataReader reader)
        {
            Asset a = new Asset()
            {
                AssetId = Convert.ToInt32(reader["asset_id"]),
                QuantityHeld = Convert.ToInt32(reader["quantity_held"]),
                Symbol = Convert.ToString(reader["symbol"]),
                CompanyName = Convert.ToString(reader["company_name"]),
                CurrentPrice = Convert.ToDecimal(reader["current_price"]),
                PortfolioID = Convert.ToInt32(reader["portfolio_id"])
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


