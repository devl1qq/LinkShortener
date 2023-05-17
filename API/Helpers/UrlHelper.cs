using System;
using System.Data;
using System.Text;

namespace API.Helpers
{
    public class UrlHelper
    {
        public string GenerateShortURL(string url)
        {
            /*// Encode the URL to Base64
            byte[] bytes = Encoding.UTF8.GetBytes(url);
            string base64String = Convert.ToBase64String(bytes);

            // Remove any special characters from the Base64 string
            string shortUrl = base64String.Replace('+', '-').Replace('/', '_').TrimEnd('=');

            return shortUrl;*/
            return url;
        }
    }
}
