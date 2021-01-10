using UserManagementSystem.DAL.Helpers;
using System.ComponentModel.DataAnnotations;

namespace UserManagementSystem.Business.Models
{
    public class UserModel : BaseModel
    {
        [Required]
        public string Username { get; set; }

        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public UserRole Role { get; set; }
    }
}
