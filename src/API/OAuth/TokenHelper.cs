using System;
using System.Text;
using System.Security.Claims;
using System.Collections.Generic;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;

namespace API.OAuth
{
    public class TokenHelper
    {
        private readonly JwtConfig _config;

        public TokenHelper(IConfiguration config)
        {
            _config = config.GetSection("JwtConfig").Get<JwtConfig>();
        }

        public string GenerateAccessToken(string id, string username, IList<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.NameIdentifier, id),
                new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddDays(3)).ToUnixTimeSeconds().ToString())
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            byte[] sekurityKey = Encoding.UTF8.GetBytes(_config.SecretKey);

            var token = new JwtSecurityToken(
                            new JwtHeader(
                                    new SigningCredentials(
                                        new SymmetricSecurityKey(sekurityKey),
                                        SecurityAlgorithms.HmacSha256
                                        )),
                            new JwtPayload(claims)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        //This method for Production
        /*
        public string GenerateAccessToken(string id, string username, IList<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.NameIdentifier, id),
                new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddDays(1)).ToUnixTimeSeconds().ToString())
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            byte[] sekurityKey = Encoding.UTF8.GetBytes(_config.SecretKey);

            var token = new JwtSecurityToken(
                            new JwtHeader(
                                    new SigningCredentials(
                                        new SymmetricSecurityKey(sekurityKey),
                                        SecurityAlgorithms.HmacSha256
                                        )),
                            new JwtPayload(_config.Issuer, _config.Issuer, claims, DateTime.Now, DateTime.Now.AddDays(1))
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }*/

        public string GenerateRefreshToken()
        {
            using (var rng = RandomNumberGenerator.Create())
            {
                var randomNumber = new byte[32];
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
    }
}
