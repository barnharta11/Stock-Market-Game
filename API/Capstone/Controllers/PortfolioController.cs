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
    public class PortfolioController : ControllerBase
    {

        private readonly IPortfolioDAO portfolioDAO;
        
        public PortfolioController(IPortfolioDAO portfolioDAO)
        {
            this.portfolioDAO = portfolioDAO;
            
        }
        [HttpGet("/{userGameID}")]
        public IActionResult ListUsersGames(int userGameID)
        {
            return Ok(portfolioDAO.GetPortfolio(userGameID));
        }
    }
}
