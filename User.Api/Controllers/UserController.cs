using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Users.Api.Models;
using Users.Api.Contexts;
using Users.Api.Controllers.Messages;

namespace Users.Api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserController : ControllerBase
    {

        private readonly ILogger<UserController> _logger;
        private readonly UserContext _userContext;

        public UserController(ILogger<UserController> logger, UserContext userContext)
        {
            _logger = logger;
            _userContext = userContext;
        }

        [HttpGet]
        public List<User> Get()
        {
            
            return _userContext.GetUsers(); 
        }

        [HttpPost]
        public int Post(CreateUserRequest request)
        {
            if(request != null)
            {
                User user = new User(request.FirstName, request.LastName, request.Email);
                return _userContext.AddUser(user);
            }
            return 0;
        }
    }
}
