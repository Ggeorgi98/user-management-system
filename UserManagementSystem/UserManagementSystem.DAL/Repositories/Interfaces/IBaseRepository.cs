using UserManagementSystem.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace UserManagementSystem.DAL.Repositories.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null);

        TEntity GetEntity(Expression<Func<TEntity, bool>> filter = null);

        void Insert(TEntity entity);
        void Update(TEntity entity);
        bool Save();
        bool Delete(TEntity entity);

        Task<bool> DoesEntityExistAsync(Guid id);
    }
}
