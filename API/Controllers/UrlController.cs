using API.Entities;
using API.Helpers;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
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
        public async Task<IActionResult> CreateShortUrl([FromBody] UrlModel model)
        {
            if (!Uri.TryCreate(model.Url, UriKind.Absolute, out Uri validatedUri))
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
                OriginalUrl = model.Url,
                CreateDate = DateTime.UtcNow,
                CreatedBy = username
            };

            await _dbContext.ShortenedUrls.AddAsync(shortenedURL);
            await _dbContext.SaveChangesAsync();

            return Ok(shortUrl);
        }


        [HttpGet("get_all_links")]
        [Authorize]

        public async Task<IActionResult> GetAllLinks()
        {

            var links =
                _dbContext.ShortenedUrls.Select(x => new ShortenedUrl()
                {
                    Id = x.Id,
                    ShortUrl = x.ShortUrl,
                    ShortUrlCode = x.ShortUrlCode,
                    OriginalUrl = x.OriginalUrl,
                    CreateDate = x.CreateDate,
                    CreatedBy = x.CreatedBy

                }).ToList();
            if (links.IsNullOrEmpty())
            {
                return BadRequest("There is no links.");

            }
            return Ok(links);
        }

        [HttpGet("get_url_info/{UrlId}")]
        [Authorize]

        public async Task<IActionResult> GetUrlInfo(int UrlId)
        {

            var link = await _dbContext.ShortenedUrls.FindAsync(UrlId);                

            if (link == null)
            {
                return BadRequest("There is no link.");

            }
            return Ok(link);
        }

        [HttpDelete("delete_link/{UrlId}")]
        [Authorize(Roles = "admin")]

        public async Task<IActionResult> DeleteLink(int UrlId)
        {
            var link = await _dbContext.ShortenedUrls.FindAsync(UrlId);
            if (link == null) return NotFound();

            _dbContext.ShortenedUrls.Remove(link);

            await _dbContext.SaveChangesAsync();

            return Ok("Link has been deleted");

        }

        [HttpGet("admin_check")]
        [Authorize]

        public async Task<IActionResult> AdminCheck()
        {
            bool result;
 
            if (User.IsInRole("admin"))
                result = true;
            else
                result = false;
            return Ok(result);
        }

    }
}
