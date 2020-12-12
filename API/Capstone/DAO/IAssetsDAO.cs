using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.DAO
{
    public interface IAssetsDAO
    {
        List<Asset> GetAssets(int userID, int gameID);
        void BuySellAsset(UpdateAsset newAsset);
        void UpdateQuantity(UpdateAsset updateAsset);



        //List<ReturnPortfolio> ListAllPortfolios();
    }
}