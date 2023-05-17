using API.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("")]
    public class RedirectingController : Controller
    {
        private readonly AppDbContext _dbContext;

        public RedirectingController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("{shortUrlCode}")]
        public IActionResult RedirectToOriginalUrl(string shortUrlCode)
        {
            var urlMatch = _dbContext.ShortenedUrls.FirstOrDefault(x => x.ShortUrlCode == shortUrlCode);

            if (urlMatch != null)
            {
                return Redirect(urlMatch.OriginalUrl);
            }

            return NotFound();
        }
    }
}
