using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Models;
using MoviesAPI.Services;
using System.Net;

namespace MoviesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {

        private readonly ILogger<UserController> _logger;
        private readonly IUserRepo _userRepo;

        public UserController(ILogger<UserController> logger, IUserRepo userRepo)
        {
            _logger = logger;
            _userRepo = userRepo;
        }

        [HttpPost(Name = "RegisterNewUser")]
        public IActionResult RegisterNewUser()
        {
            try
            {
                var newUserId = _userRepo.RegisterNewUser();

                return Ok(newUserId);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Exception occurred while registering new user");
                return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
