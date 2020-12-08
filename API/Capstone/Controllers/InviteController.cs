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
    public class InviteController : ControllerBase
    {
        private readonly IUserGameDAO userGameDAO;

        public InviteController(IUserGameDAO _userGameDAO)
        {
            userGameDAO = _userGameDAO;
        }


        [HttpPost("/invite")]
        public IActionResult InviteUser(int userId, int gameId)
        {            
            IActionResult result; 
            string userGameId = Convert.ToString(userGameDAO.InviteUser(userId, gameId));
            if (userGameId != null)
            {
                result = Created(userGameId, null);
            }
            else
            {
                result = BadRequest(new { message = "An error occurred and game was not created." });
            }

            return result;
        }
    }
}
