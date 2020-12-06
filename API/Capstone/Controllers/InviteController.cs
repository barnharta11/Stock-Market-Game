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
            //createGame.CreatorId = userDAO.GetUser(createGame.CreatorName).UserId;
            IActionResult result;

            //Game existingGame = gameDAO.GetGame(createGame.GameName);
            //if (existingGame != null)
            //{
            //    return Conflict(new { message = "Game name already taken. Please choose a different game name." });
            //}

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
