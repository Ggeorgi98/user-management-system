using System.ComponentModel.DataAnnotations;
using UserManagementSystem.DAL.Helpers;

namespace UserManagementSystem.DAL.Entities
{
    public class User : BaseEntity
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string FullName { get; set; }

        public bool IsBlackListed { get; set; }

        [Required]
        public UserRole Role { get; set; }
    }
}
