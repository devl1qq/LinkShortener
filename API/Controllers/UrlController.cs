using API.Entities;
using API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.CodeDom.Compiler;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UrlController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _dbContext;

        public UrlController(IConfiguration configuration, AppDbContext dbContext)
        {
            _configuration = configuration;
            _dbContext = dbContext;
        }

        [HttpPost("create_short_url")]
        [Authorize]
        public IActionResult CreateShortUrl(string url)
        {
            if (string.IsNullOrEmpty(url)){
                return BadRequest("URL cannot be empty");
            }
            var UrlHelper = new UrlHelper();
            string ShortUrl = UrlHelper.GenerateShortURL(url);
            var shortenedURL = new ShortenedUrl
            {
                ShortUrl = ShortUrl,
                OriginalUrl = url,
                CreateDate = DateTime.UtcNow,
                CreatedBy = "Me"
            };

            _dbContext.ShortenedUrls.Add(shortenedURL);
            _dbContext.SaveChanges();
            return Ok(ShortUrl);
        }
    }
}
