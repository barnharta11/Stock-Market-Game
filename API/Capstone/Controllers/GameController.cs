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
        private readonly IUserDAO userDAO;
        public GameController(IGameDAO _gameDAO, IUserDAO _userDAO)
        {
            userDAO = _userDAO;
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
            createGame.CreatorId = GetUserId();
            IActionResult result;

            Game existingGame = gameDAO.GetGame(createGame.GameName);
            if (existingGame != null)
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
        [HttpGet("/games")]
        public IActionResult ListAllGames()
        {
            return Ok(gameDAO.GetGameList());
        }

        [HttpGet("/leaderboard")]
        public IActionResult ListLeaderboard()
        {
            return Ok(gameDAO.GetLeaderboard());
        }

        [HttpGet("/games/{userId}")]
        public IActionResult ListUsersGames(int userId)
        {
            return Ok(gameDAO.GetGamesByUser(userId));
        }
        private int GetUserId()
        {
            string strUserId = User.Claims.FirstOrDefault(claim => claim.Type == "sub")?.Value;
            return String.IsNullOrEmpty(strUserId) ? 0 : Convert.ToInt32(strUserId);
        }
    }
}
