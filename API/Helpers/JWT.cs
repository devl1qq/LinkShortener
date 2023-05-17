using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Entities;
using Microsoft.IdentityModel.Tokens;

namespace API.Helpers
{
    public class JwtHelper
    {
        public string GenerateJwtToken(User user)
        {
            // Set the secret key used for signing the token (keep it secure and secret)
            var key = Encoding.ASCII.GetBytes("supersecretkeydaaamnboy3214432eds");

            // Set the token's expiration time (e.g., 1 day from now)
            var expirationTime = DateTime.UtcNow.AddDays(1);

            // Create the token descriptor with the user claims
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = expirationTime,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            // Create the token handler
            var tokenHandler = new JwtSecurityTokenHandler();

            // Generate the JWT token
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // Serialize the token to a string
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }
    }
}
