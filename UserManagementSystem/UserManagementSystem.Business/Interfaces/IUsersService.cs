using UserManagementSystem.Business.Models;
using UserManagementSystem.DAL.Entities;
using System;

namespace UserManagementSystem.Business.Interfaces
{
    public interface IUsersService : IBaseService<User, UserResponseModel, UserModel>
    {
        PagedListModel<UserResponseModel> GetPagedUsers(UserRequestListModel model);

        bool ChangePassword(Guid userId, ChangePasswordModel model);

        UserResponseModel ValidateEmailAndPassword(string email, string password);
    }
}
