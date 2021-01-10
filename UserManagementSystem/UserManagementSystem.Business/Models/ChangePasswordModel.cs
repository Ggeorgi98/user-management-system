using System;
using System.ComponentModel.DataAnnotations;

namespace UserManagementSystem.Business.Models
{
    public class ChangePasswordModel
    {
        [Required]
        public Guid UserID { get; set; }

        [Required]
        public string NewPassword { get; set; }

        [Required]
        public string ConfirmPassword { get; set; }
    }
}
