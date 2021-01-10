using UserManagementSystem.Business.Models;
using FluentValidation;
using UserManagementSystem.WebAPI.PasswordLists;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace UserManagementSystem.WebAPI.Infrastructure
{
    public class ChangePasswordModelValidator : AbstractValidator<ChangePasswordModel>
    {
        static HashSet<string> Passwords { get; set; } = PasswordListLoader.Top100000Passwords.Value;

        public ChangePasswordModelValidator()
        {
            RuleFor(x => x.NewPassword)
               .Must(m => ValidatePassword(m))
               .WithMessage("Password should include at least one uppercase, one small letter and one number");

            RuleFor(x => x.ConfirmPassword)
                .Equal(m => m.NewPassword)
                .WithMessage("Confirm password should be equal to new password");

            RuleFor(x => x.NewPassword)
                .Must(m => ValidatePasswordIsCommon(m))
                .WithMessage("The chosen password is one of the most common ones. Please, choose another one.");
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

        private bool ValidatePasswordIsCommon(string password)
        {
            if (Passwords.Contains(password))
            {
                return false;
            }

            return true;
        }
    }
}
