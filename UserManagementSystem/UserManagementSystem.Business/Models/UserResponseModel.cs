using UserManagementSystem.DAL.Helpers;

namespace UserManagementSystem.Business.Models
{
    public class UserResponseModel : BaseResponseModel
    {
        public string Email { get; set; }

        public string Username { get; set; }

        public string FullName { get; set; }

        public bool IsBlackListed { get; set; }

        public UserRole Role { get; set; }
    }
}
