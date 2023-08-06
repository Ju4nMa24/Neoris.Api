using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Neoris.Abstractions.Repositories.Auth;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Neoris.Commons.Repositories
{
#nullable disable
    public class AuthRepository : IAuthenticationRepository
    {
        private IConfiguration _configuration { get; }
        public AuthRepository(IConfiguration configuration) => _configuration = configuration;
        /// <summary>
        /// JWT Create
        /// </summary>
        /// <param name="auth"></param>
        /// <returns></returns>
        public Task<string> Generate(string tokenId)
        {
            try
            {
                byte[] key = Encoding.ASCII.GetBytes(_configuration["JwtParameters:SecretKey"].ToString());
                SecurityTokenDescriptor tokenDescriptor = new()
                {
                    Issuer = JsonConvert.SerializeObject(tokenId),
                    Subject = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, tokenId),
                        new Claim(ClaimTypes.Role, DateTime.UtcNow.ToString()),
                    }),
                    NotBefore = DateTime.UtcNow,
                    Audience = _configuration["JwtParameters:Audience"],
                    IssuedAt = DateTime.UtcNow,
                    Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["JwtParameters:TimeOut"].ToString())),
                    SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
                };
                JwtSecurityTokenHandler tokenHandler = new();
                SecurityToken securityToken = tokenHandler.CreateToken(tokenDescriptor);
                return Task.Run(() => tokenHandler.WriteToken(securityToken));
            }
            catch (Exception ex)
            {
                return Task.Run(() => ex.Message);
            }
        }
    }
}
