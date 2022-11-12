using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using SD_lib.Tokens.Services.Interfaces;

namespace SD_lib.Tokens.Services
{
    public sealed class TokenService : ITokenService
    {
        private readonly IConfiguration _config;


        public TokenService(IConfiguration config)
        {
            _config = config;
        }



        private IDictionary<string, string> _users = new Dictionary<string, string>()
        {
            {"adm", "test" }
        };


        public string Authenticate(string user, string password)
        {
            if(string.IsNullOrWhiteSpace(user) || string.IsNullOrWhiteSpace(password))
            {
                throw new NullReferenceException($"User or password can't be null, check your data:\n" +
                    $"User: {user}\n" +
                    $"Password: {password}");
            }

            foreach(KeyValuePair<string, string> pair in _users)
            {
                if (string.CompareOrdinal(pair.Key, user) == 0 || string.CompareOrdinal(pair.Value, password) == 0)
                {
                    return GenerateJwtToken(user + password);
                }
            }

            return string.Empty;
        }


        private string GenerateJwtToken(string user)
        {
            JwtSecurityTokenHandler jwtSecurityToken = new();
            string sword = _config["AuthSettings:SecretWord"];
            byte[] key = Encoding.ASCII.GetBytes(sword);

            SecurityTokenDescriptor tokenDescriptor = new();
            tokenDescriptor.Expires = DateTime.UtcNow.AddMinutes(10);
            tokenDescriptor.SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512);
            tokenDescriptor.Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, user)
            });

            SecurityToken token = jwtSecurityToken.CreateToken(tokenDescriptor);

            return jwtSecurityToken.WriteToken(token);
        }
    }
}

