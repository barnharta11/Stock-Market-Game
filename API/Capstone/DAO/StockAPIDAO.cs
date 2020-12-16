using Capstone.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO
{
    public class StockAPIDAO : IStockAPIDAO
    {
        string URL = "/market/v2/get-quotes?symbols=AAPL%2CAMZN%2CCRM%2CMSFT%2CIBM%2CPLTR%2CINTC%2CGOOG%2CTSLA%2CNVDA%2CFB%2CHPE%2CCSCO%2CZM%2CVMW&region=US";
        string host = "https://apidojo-yahoo-finance-v1.p.rapidapi.com";
        string API_KEY = "95b93fc1b3mshd730d053c0ba189p109c00jsn36d6d75341d3";
        string HOST_HEADER = "apidojo-yahoo-finance-v1.p.rapidapi.com";

        private readonly string connectionString;

        public StockAPIDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        private RestClient client;
        public List<Asset> GetStocks()
        {
            bool notUpToDate = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand selectCommand = new SqlCommand("select time_updated from assets where asset_id=2", conn);
                    DateTime assetOneTime = Convert.ToDateTime(selectCommand.ExecuteScalar());
                    if ((DateTime.Compare(DateTime.Now, assetOneTime.AddHours(1))) > 0)
                    {
                        notUpToDate = true;
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }

            if (notUpToDate)
            {
                client = new RestClient(host);
                client.AddDefaultHeader("X-Rapidapi-Host", HOST_HEADER);
                client.AddDefaultHeader("X-Rapidapi-Key", API_KEY);

                RestRequest request = new RestRequest(URL);
                IRestResponse<StockAPI> response = client.Get<StockAPI>(request);
                //IRestResponse response = client.Get(request);

                if (response.ResponseStatus != ResponseStatus.Completed)
                {
                    throw new Exception("Error occurred - unable to reach server.", response.ErrorException);
                }
                else if (!response.IsSuccessful)
                {
                    throw new Exception("Error occurred - received non-success response: " + (int)response.StatusCode);
                }

                else
                {
                    return LoadThenReturnStocks(response.Data.quoteResponse.result);
                }
            }
            else
            {
                return ReturnStocks();
            }

        }
        public List<Asset> ReturnStocks()
        {
            List<Asset> returnList = new List<Asset>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand returnCommand = new SqlCommand("Select * from assets", conn);
                SqlDataReader reader = returnCommand.ExecuteReader();
                while (reader.Read())
                {
                    Asset readAsset = new Asset();
                    readAsset.AssetId = Convert.ToInt32(reader["asset_id"]);
                    readAsset.CompanyName = Convert.ToString(reader["company_name"]);
                    readAsset.CurrentPrice = Convert.ToDecimal(reader["current_price"]);
                    readAsset.Symbol = Convert.ToString(reader["symbol"]);
                    returnList.Add(readAsset);
                }
            }

            return returnList;
        }
        public List<Asset> LoadThenReturnStocks(List<Result> stockAPIList)
        {
            List<Asset> returnList = new List<Asset>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand selectCommand = new SqlCommand("select asset_id from assets where asset_id=2", conn);
                    int? assetOne = Convert.ToInt32(selectCommand.ExecuteScalar());

                    foreach (Result toInsert in stockAPIList)
                    {
                        if (assetOne == 0)
                        {
                            SqlCommand insertCommand = new SqlCommand("INSERT INTO assets (symbol, company_name, current_price, time_updated) Values(@symbol, @company_name, @current_price, @time_updated)", conn);
                            insertCommand.Parameters.AddWithValue("@symbol", toInsert.symbol);
                            insertCommand.Parameters.AddWithValue("@company_name", toInsert.shortName);
                            insertCommand.Parameters.AddWithValue("@current_price", toInsert.regularMarketPrice);
                            insertCommand.Parameters.AddWithValue("@time_updated", DateTime.Now);
                            insertCommand.ExecuteNonQuery();
                        }
                        else
                        {


                            SqlCommand updateCommand = new SqlCommand("Update assets Set current_price = @current_price, time_updated=@time_updated where symbol=@symbol", conn);
                            updateCommand.Parameters.AddWithValue("@current_price", toInsert.regularMarketPrice);
                            updateCommand.Parameters.AddWithValue("@symbol", toInsert.symbol);
                            updateCommand.Parameters.AddWithValue("@time_updated", DateTime.Now);
                            updateCommand.ExecuteNonQuery();


                        }

                    }
                    SqlCommand returnCommand = new SqlCommand("Select * from assets", conn);
                    SqlDataReader reader = returnCommand.ExecuteReader();
                    while (reader.Read())
                    {
                        Asset readAsset = new Asset();
                        readAsset.AssetId = Convert.ToInt32(reader["asset_id"]);
                        readAsset.CompanyName = Convert.ToString(reader["company_name"]);
                        readAsset.CurrentPrice = Convert.ToDecimal(reader["current_price"]);
                        readAsset.Symbol = Convert.ToString(reader["symbol"]);
                        returnList.Add(readAsset);
                    }


                }
            }
            catch (SqlException)
            {
                throw;
            }
            return returnList;
        }
    }
}
