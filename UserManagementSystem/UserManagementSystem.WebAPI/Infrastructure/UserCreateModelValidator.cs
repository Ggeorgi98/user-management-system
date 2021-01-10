using UserManagementSystem.Business.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace UserManagementSystem.WebAPI.Infrastructure
{
    public class UserCreateModelValidator : AbstractValidator<UserCreateModel>
    {
        public UserCreateModelValidator()
        {
            RuleFor(x => x.Password)
                .NotNull()
                .WithMessage("Password is required")
                .Must(m => ValidatePassword(m))
                .WithMessage("Password should include at least one uppercase, one small letter and one number. The length should be atleast 8 symbols");
        }

        private bool ValidatePassword(string password)
        {            
            if (string.IsNullOrEmpty(password))
            {
                return false;
            }

            //without special characters
            var reg = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$");

            //with special characters too
            //var reg = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$");


            return reg.Match(password).Success;
        }
    }
}
