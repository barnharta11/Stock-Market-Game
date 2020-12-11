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
        
        public AssetsController(IAssetsDAO assetsDAO)
        {
            this.assetsDAO = assetsDAO;
            
        }
        [HttpGet("/assets/{userID}/{gameID}")]
        public IActionResult ListAssets(int userID, int gameID)
        {
            return Ok(assetsDAO.GetAssets(userID, gameID));
        }
    }
}
