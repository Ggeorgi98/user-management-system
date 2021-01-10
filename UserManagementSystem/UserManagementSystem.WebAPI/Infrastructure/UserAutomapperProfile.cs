using AutoMapper;
using UserManagementSystem.Business.Models;
using UserManagementSystem.DAL.Entities;

namespace UserManagementSystem.WebAPI.Infrastructure
{
    public class UserAutomapperProfile : Profile
    {
        public UserAutomapperProfile()
        {
            CreateMap<UserResponseModel, User>();
            CreateMap<User, UserResponseModel>();
            CreateMap<UserModel, User>();
            CreateMap<UserCreateModel, User>();
        }
    }
}
