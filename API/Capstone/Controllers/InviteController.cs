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
    public class InviteController : ControllerBase
    {
        private readonly IUserGameDAO userGameDAO;

        public InviteController(IUserGameDAO _userGameDAO)
        {
            userGameDAO = _userGameDAO;
        }


        [HttpPost("/invite")]
        public IActionResult PostInvite(Invite inviteRequest)
        {            
            IActionResult result; 
            int? userGameId = (userGameDAO.InviteUser(inviteRequest));
            if (userGameId != null)
            {
                result = Created(userGameId.ToString(), null);
            }
            else
            {
                result = BadRequest(new { message = "An error occurred and game was not created." });
            }

            return result;
        }
    }
}
