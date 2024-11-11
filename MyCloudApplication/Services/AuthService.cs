using Mapster;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MyCloudApplication.DTOs.User;
using MyCloudApplication.Interfaces;
using MyCloudDomain.Auth;
using MyCloudDomain.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MyCloudApplication.Services
{
    public class AuthService(IAuthRepository authRepository, IConfiguration configuration) : IAuth
    {
        private readonly IAuthRepository _authRepository = authRepository;
        private readonly IConfiguration _configuration = configuration;
        public async Task<string> getAuthentication(AuthRequestDTO authRequest)
        {
            try
            {
                var user = await _authRepository.getUserByUsername(authRequest.UserName);
                if (user == null || !user.CheckPassword(authRequest.Password))
                {
                    return null;
                }

                var jwtHandler = new JwtSecurityTokenHandler();
                string getKey = _configuration.GetSection("Jwt").GetSection("SecretKey").Value;
                var key = Encoding.ASCII.GetBytes(getKey);
                var identity = new ClaimsIdentity(new Claim[]
                {
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(ClaimTypes.Name, user.UserName),
                    //new Claim(ClaimTypes.NameIdentifier, user.CodeEmployee)
                });

                var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

                var tokenDescriptor = new SecurityTokenDescriptor()
                {
                    Subject = identity,
                    Expires = DateTime.Now.AddMinutes(30),
                    SigningCredentials = credentials
                };
                var token = jwtHandler.CreateToken(tokenDescriptor);

                return jwtHandler.WriteToken(token);
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        string HashPassword(string password, out byte[] salt)
        {
            salt = RandomNumberGenerator.GetBytes(32);

            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                350000, //iteration
                HashAlgorithmName.SHA512,
                32);

            return Convert.ToHexString(hash);
        } 

        public async Task<bool> CreateUserLogin(CreateUserLoginDTO userDTO)
        {
            var passwordHash = HashPassword(userDTO.Password, out var salt);
            var user = new AuthRequestEntity(userDTO.UserName, passwordHash, Convert.ToHexString(salt), userDTO.Role);

            return await _authRepository.createUserLogin(user.Adapt<CreateUserLoginEntitiy>());
        }
    }
}
