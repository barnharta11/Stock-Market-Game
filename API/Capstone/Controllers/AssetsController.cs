using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.DAO;
using Capstone.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Capstone.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AssetsController : ControllerBase
    {

        private readonly IAssetsDAO assetsDAO;
        private IStockAPIDAO stockAPIDAO;

        public AssetsController(IAssetsDAO assetsDAO, IStockAPIDAO stockAPIDAO)
        {
            this.assetsDAO = assetsDAO;
            this.stockAPIDAO = stockAPIDAO;
        }
        [HttpGet("/assets/{userID}/{gameID}")]
        public List<List<Asset>> ListAssets(int userID, int gameID)
        {
            List<List<Asset>> returnResults = new List<List<Asset>>();
            returnResults.Add(stockAPIDAO.GetStocks());
            returnResults.Add(assetsDAO.GetAssets(userID, gameID));
            
            
            return returnResults;
        }
    }
}
