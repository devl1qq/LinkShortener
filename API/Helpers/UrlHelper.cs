using System;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace API.Helpers
{
    public class UrlHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UrlHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public string[] GenerateShortURL()
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPORSTUVWXYZ1234567890@az";
            var urlCode = new string(Enumerable.Repeat(chars, 8)
                .Select(x => x[random.Next(x.Length)]).ToArray());
            var request = _httpContextAccessor.HttpContext.Request;
            var url = $"{request.Scheme}://{request.Host}/{urlCode}";
            return new string[] { url, urlCode };
        }
    }
}
