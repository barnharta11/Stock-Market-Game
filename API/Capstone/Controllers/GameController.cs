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
    public class GameController : ControllerBase
    {

        private readonly IGameDAO gameDAO;

        public GameController(IGameDAO _gameDAO)
        {

            gameDAO = _gameDAO;
        }

        [HttpGet("/gameName")]
        public IActionResult GetGame(string gameName)
        {
            
            IActionResult result = NotFound(new { message = "Game not found." });

            // Get the user by username
            Game game = gameDAO.GetGame(gameName);
            if (game != null)
            {
                // Switch to 200 OK
                result = Ok(game);

            }
            
            return result;

        }

        [HttpPost("/createGame")]
        public IActionResult AddGame(Game createGame)
        {
            IActionResult result;

            Game existingGame = gameDAO.GetGame(createGame.GameName);
            if (existingGame.GameName == createGame.GameName)
            {
                return Conflict(new { message = "Game name already taken. Please choose a different game name." });
            }

            Game game = gameDAO.AddGame(createGame.GameName, createGame.CreatorId, createGame.StartDate, createGame.EndDate);
            if (game != null)
            {
                result = Created(game.GameName, null); 
            }
            else
            {
                result = BadRequest(new { message = "An error occurred and game was not created." });
            }

            return result;
        }
    }
}
