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
    public class UserController : ControllerBase
    {
        private readonly IUserDAO userDAO;

        public UserController(IUserDAO _userDAO)
        {
            userDAO = _userDAO;
        }
        [HttpGet("/user")]
        public IActionResult ListAllUsers()
        {
            return Ok(userDAO.ListAllUsers());
        }
    }
}
