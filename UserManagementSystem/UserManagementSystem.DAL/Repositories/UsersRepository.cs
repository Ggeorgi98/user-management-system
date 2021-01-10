using UserManagementSystem.DAL.Context;
using UserManagementSystem.DAL.Entities;
using UserManagementSystem.DAL.Repositories.Interfaces;

namespace UserManagementSystem.DAL.Repositories
{
    public class UsersRepository : BaseRepository<User>, IUsersRepository
    {
        public UsersRepository(UserManagementSystemContext dbContext) : base(dbContext)
        {
        }
    }
}
