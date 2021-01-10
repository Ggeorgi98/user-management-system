using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace UserManagementSystem.Business.Interfaces
{
    public interface ILoggedUserHelper
    {
        Task<string> GetLoggedUserIdAsync(HttpContext httpContext);
        
        Task<string> GetLoggedUserRoleAsync(HttpContext httpContext);
    }
}
