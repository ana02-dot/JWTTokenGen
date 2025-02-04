using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebToken
{
    public class JWTService
    {
        private readonly string _key = "my_secret_key_12345";
        public string GenerateToken(string username)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: "https://localhost:7060",
                audience: "https://localhost:7060",
                claims: claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: cred
                );

            return new JwtSecurityTokenHandler().WriteToken(token);

        } 
    } 
}
