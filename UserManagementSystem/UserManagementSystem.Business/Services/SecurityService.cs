using UserManagementSystem.Business.Interfaces;
using UserManagementSystem.Business.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace UserManagementSystem.Business.Services
{
    public class SecurityService : ISecurityService
    {
        private readonly IUsersService _usersService;
        private readonly IConfiguration _configuration;
        public SecurityService(IUsersService usersService, IConfiguration configuration)
        {
            _usersService = usersService;
            _configuration = configuration;
        }

        public Task<UserResponseModel> ValidateEmailAndPassword(string email, string password)
        {
            var user = _usersService.GetResponseModel(x => x.Email == email && x.Password == password);

            if (user == null)
                return Task.FromResult<UserResponseModel>(null);

            return Task.FromResult(user);
        }

        public Task<string> GenerateToken(UserResponseModel user)
        {
            var claims = PopulateTokenClaims(user);

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokeOptions = new JwtSecurityToken(
               issuer: _configuration["Jwt:Issuer"],
               audience: _configuration["Jwt:Issuer"],
               claims: claims,
               expires: DateTime.Now.AddMinutes(30),
               signingCredentials: signinCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);

            return Task.FromResult(tokenString);
        }

        private List<Claim> PopulateTokenClaims(UserResponseModel user)
        {
            return new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };
        }
    }
}
