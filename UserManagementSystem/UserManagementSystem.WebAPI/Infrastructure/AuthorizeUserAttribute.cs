using UserManagementSystem.DAL.Helpers;
using Microsoft.AspNetCore.Authorization;
using System;

namespace UserManagementSystem.WebAPI.Infrastructure
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class AuthorizeUserAttribute : AuthorizeAttribute
    {
        public UserRole Permission { get; private set; }

        public AuthorizeUserAttribute(UserRole permission)
        {
            Roles = permission.ToString();
        }
    }
}
