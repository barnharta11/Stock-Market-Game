using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.DAO
{
    public interface IAssetsDAO
    {
        List<Asset> GetAssets(int userID, int gameID);
        //List<ReturnPortfolio> ListAllPortfolios();
    }
}