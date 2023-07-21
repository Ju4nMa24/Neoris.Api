using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Neoris.Abstractions.Repositories.Auth;
using Newtonsoft.Json;
using System.Buffers.Binary;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Neoris.Commons.Repositories
{
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
        /// <summary>
        /// Method created for encryption and key generation.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private string GenerateKey(string request)
        {
            RSA rsa = RSA.Create();
            byte[] key = Encoding.UTF8.GetBytes(Convert.ToBase64String(rsa.ExportRSAPrivateKey()).Substring(DateTime.UtcNow.Minute, 32));
            byte[] bytes = Encoding.UTF8.GetBytes(request);
            int nonceSize = AesGcm.NonceByteSizes.MaxSize;
            int tagSize = AesGcm.TagByteSizes.MaxSize;
            int cipherSize = bytes.Length;
            int dataLength = 4 + nonceSize + 4 + tagSize + cipherSize;
            Span<byte> resultData = dataLength < 1024 ? stackalloc byte[dataLength] : new byte[dataLength].AsSpan();
            BinaryPrimitives.WriteInt32LittleEndian(resultData.Slice(0, 4), nonceSize);
            BinaryPrimitives.WriteInt32LittleEndian(resultData.Slice(4 + nonceSize, 4), nonceSize);
            Span<byte> nonce = resultData.Slice(4, nonceSize);
            RandomNumberGenerator.Fill(nonce);
            using AesGcm aes = new AesGcm(key);
            aes.Encrypt(nonce, bytes.AsSpan(), resultData.Slice(4 + nonceSize + 4 + tagSize, cipherSize), resultData.Slice(4 + nonceSize + 4, tagSize));
            return Convert.ToBase64String(resultData);
        }
    }
}
