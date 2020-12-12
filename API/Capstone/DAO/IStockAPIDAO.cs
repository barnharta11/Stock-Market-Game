using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.DAO
{
    public interface IStockAPIDAO
    {
        List<Asset> GetStocks();
        List<Asset> LoadThenReturnStocks(List<Result> stockAPIList);
    }
}