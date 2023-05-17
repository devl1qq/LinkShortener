using API.Entities;
using API.Helpers;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public UserController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("register")]
        public IActionResult Register(UserModel model)
        {

            var existingUser = _dbContext.Users.FirstOrDefault(u => u.Username == model.Username);
            if (existingUser != null)
            {

                return BadRequest("Username already exists");
            }

            var user = new User
            {
                Username = model.Username,
                Password = model.Password,
                Role = "user"
            };

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            return Ok("User registered successfully");
        }

        [HttpPost("login")]
        public IActionResult Login(UserModel model)
        {
            // Find the user by username
            var user = _dbContext.Users.FirstOrDefault(u => u.Username == model.Username);

            if (user == null)
            {
                // User not found, return error response
                return BadRequest("Invalid username or password");
            }

            if (user.Password != model.Password)
            {
                // Password is incorrect, return error response
                return BadRequest("Invalid username or password");
            }

            var jwtHelper = new JwtHelper();
            var token = jwtHelper.GenerateJwtToken(user);

            // Return the token as a response
            return Ok(new { token });
        }


    }
}