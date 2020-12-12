using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.DAO;
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
        public List<IActionResult> ListAssets(int userID, int gameID)
        {
            List<IActionResult> returnResults = new List<IActionResult>();
            returnResults.Add(Ok(assetsDAO.GetAssets(userID, gameID)));
            returnResults.Add(Ok(stockAPIDAO.GetStocks()));
            return returnResults;
        }
    }
}
