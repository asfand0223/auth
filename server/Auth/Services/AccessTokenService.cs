using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Auth.Configuration;
using Auth.Interfaces.Services;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Auth.Services
{
    public class AccessTokenService : IAccessTokenService
    {
        private readonly IOptions<Config> _c;

        public AccessTokenService(IOptions<Config> c)
        {
            _c = c;
        }

        public string Generate(Guid userId, string username)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_c.Value.Jwt.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _c.Value.Jwt.Issuer,
                audience: _c.Value.Jwt.Audience,
                claims: new List<Claim>
                {
                    new Claim("user_id", $"{userId}"),
                    new Claim("username", $"{username}"),
                },
                expires: DateTime.Now.AddMinutes(_c.Value.Jwt.Expires.TotalSeconds),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
