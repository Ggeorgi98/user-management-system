using AutoMapper;
using UserManagementSystem.Business.Interfaces;
using UserManagementSystem.Business.Models;
using UserManagementSystem.DAL.Entities;
using UserManagementSystem.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UserManagementSystem.Business.Services
{
    public class UsersService : BaseService<User, UserResponseModel, UserModel>, IUsersService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;

        public UsersService(IUsersRepository usersRepository, IMapper mapper)
            : base(usersRepository, mapper)
        {
            _usersRepository = usersRepository;
            _mapper = mapper;
        }

        public PagedListModel<UserResponseModel> GetPagedUsers(UserRequestListModel model)
        {
            var entities = base.GetAll(u => string.IsNullOrEmpty(model.Name)
                        || (u.FullName).Contains(model.Name));

            if (!entities.Any())
                return null;

            var result = entities;

            if (model.ItemsPerPage.Value > 0 && model.PageNumber.Value > 0)
            {
                result = result.Skip((model.PageNumber.Value - 1) * model.ItemsPerPage.Value).Take(model.ItemsPerPage.Value);
            }

            return new PagedListModel<UserResponseModel>(result.ToList(), entities.Count(),
                model.PageNumber.Value, model.ItemsPerPage.Value);
        }

        public bool ChangePassword(Guid userId, ChangePasswordModel model)
        {
            var user = _usersRepository.GetEntity(x => x.Id == userId);

            user.Password = model.NewPassword;

            try
            {
                _usersRepository.Update(user);

                if (!_usersRepository.Save())
                {
                    _validationDictionary.AddModelError(nameof(ChangePassword), "There was a problem while trying to change password");
                    return false;
                }
            }
            catch (Exception ex)
            {
                _validationDictionary.AddModelError(nameof(ChangePassword), "There was a problem while trying to change password");
                return false;
            }

            return true;
        }

        public UserResponseModel ValidateEmailAndPassword(string email, string password)
        {
            var user = GetResponseModel(x => x.Email == email && x.Password == password);

            if (user == null)
                return null;

            return user;
        }

        public override UserResponseModel Update(Guid id, UserModel model)
        {
            var existingEmail = GetResponseModel(u => u.Email == model.Email && u.Id != id);
            if (existingEmail != null)
            {
                _validationDictionary.AddModelError(nameof(model.Email), "User with this email exists");

                return null;
            }

            return base.Update(id, model);
        }
    }
}
