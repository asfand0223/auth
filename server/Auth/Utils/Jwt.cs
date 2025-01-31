using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using AR = Auth.Results;

namespace Auth.Utils
{
    public static class Jwt
    {
        public static string Generate(
            string secret_key,
            string issuer,
            string audience,
            List<Claim> claims,
            double expiresIn
        )
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret_key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddSeconds(expiresIn),
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static Results.TokenValidationResult Validate(
            string token,
            string secret_key,
            string issuer,
            string audience
        )
        {
            try
            {
                ClaimsPrincipal claimsPrincipal = new JwtSecurityTokenHandler().ValidateToken(
                    token,
                    new TokenValidationParameters()
                    {
                        ValidateLifetime = true,
                        ValidateAudience = true,
                        ValidateIssuer = true,
                        ValidIssuer = issuer,
                        ValidAudience = audience,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(secret_key)
                        ),
                        ClockSkew = TimeSpan.Zero,
                    },
                    out var _
                );
                return new AR.TokenValidationResult { Valid = true, Expired = false };
            }
            catch (SecurityTokenExpiredException)
            {
                return new AR.TokenValidationResult { Valid = true, Expired = true };
            }
            catch (SecurityTokenValidationException)
            {
                return new AR.TokenValidationResult { Valid = false, Expired = false };
            }
        }
    }
}
