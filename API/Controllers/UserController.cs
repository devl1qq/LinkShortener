using API.Entities;
using API.Helpers;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public UserController(AppDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
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
            var user = _dbContext.Users.FirstOrDefault(u => u.Username == model.Username);

            if (user == null)
            {
                return BadRequest("Invalid username or password");
            }

            if (user.Password != model.Password)
            {
                return BadRequest("Invalid username or password");
            }

            var jwtHelper = new JwtHelper(_configuration);
            var token = jwtHelper.GenerateJwtToken(user);

            return Ok(new { token });
        }
    }
}
