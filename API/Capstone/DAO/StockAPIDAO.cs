using Capstone.Models;
using RestSharp;
using System;
using System.Collections.Generic;
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

        private RestClient client;

        public StockAPI GetStocks()
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
                return response.Data;
            }
        }
    }
}
