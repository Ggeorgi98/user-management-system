using UserManagementSystem.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace UserManagementSystem.Business.Interfaces
{
    public interface ISecurityService
    {
        Task<UserResponseModel> ValidateEmailAndPassword(string email, string password);

        Task<string> GenerateToken(UserResponseModel user);
    }
}
