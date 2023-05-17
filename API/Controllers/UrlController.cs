using API.Entities;
using API.Helpers;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Web;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UrlController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _dbContext;
        private readonly UrlHelper _urlHelper;

        public UrlController(IConfiguration configuration, AppDbContext dbContext, UrlHelper urlHelper)
        {
            _configuration = configuration;
            _dbContext = dbContext;
            _urlHelper = urlHelper;
        }

        [HttpPost("create_short_url")]
        [Authorize]
        public async Task<IActionResult> CreateShortUrl(string originalUrl)
        {
            if (!Uri.TryCreate(originalUrl, UriKind.Absolute, out Uri validatedUri))
            {
                return BadRequest("Invalid URL format.");
            }

            var username = User.Identity.Name;
            string[] generatedUrl = _urlHelper.GenerateShortURL();
            string shortUrl = generatedUrl[0];
            string shortUrlCode = generatedUrl[1];

            var shortenedURL = new ShortenedUrl
            {
                ShortUrl = shortUrl,
                ShortUrlCode = shortUrlCode,
                OriginalUrl = originalUrl,
                CreateDate = DateTime.UtcNow,
                CreatedBy = username
            };

            await _dbContext.ShortenedUrls.AddAsync(shortenedURL);
            await _dbContext.SaveChangesAsync();

            return Ok(shortUrl);
        }

    }
}
