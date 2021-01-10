using UserManagementSystem.Business.Models;
using UserManagementSystem.DAL.Helpers;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;

namespace UserManagementSystem.WebAPI.Infrastructure
{
    public class BaseModelValidator : AbstractValidator<UserModel>
    {
        public BaseModelValidator(IHttpContextAccessor httpContext)
        {
            RuleFor(x => x.Role)
                .Must(ur => ValidateUserRole(ur, httpContext.HttpContext.User))
                .WithMessage(m => $"You don't have rights to change your role to {m.Role.ToString()}");
        }

        private bool ValidateUserRole(UserRole userRole, ClaimsPrincipal user)
        {
            var loggedUserRole = user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;

            if ((!string.IsNullOrEmpty(loggedUserRole) && userRole == UserRole.Admin && loggedUserRole != UserRole.Admin.ToString()) ||
                (!string.IsNullOrEmpty(loggedUserRole) && loggedUserRole == UserRole.User.ToString() && userRole != UserRole.User))
            {
                return false;
            }

            return true;
        }
    }
}
