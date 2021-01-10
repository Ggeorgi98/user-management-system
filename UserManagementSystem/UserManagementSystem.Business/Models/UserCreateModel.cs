using System.ComponentModel.DataAnnotations;

namespace UserManagementSystem.Business.Models
{
    public class UserCreateModel : UserModel
    {
        [Required]
        public string Password { get; set; }

        [Required]
        public bool IsBlackListed { get; set; }
    }
}
