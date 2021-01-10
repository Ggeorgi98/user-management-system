using UserManagementSystem.Business.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace UserManagementSystem.Business.Services
{
    public class LoggedUserHelper : ILoggedUserHelper
    {
        public async Task<string> GetLoggedUserIdAsync(HttpContext httpContext)
        {
            return await Task.FromResult(httpContext.User?.Claims?.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti)?.Value);
        }

        public async Task<string> GetLoggedUserRoleAsync(HttpContext httpContext)
        {
            return await Task.FromResult(httpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value);
        }
    }
}
