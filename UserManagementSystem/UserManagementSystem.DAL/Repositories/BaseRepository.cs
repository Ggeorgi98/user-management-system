using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using UserManagementSystem.DAL.Context;
using UserManagementSystem.DAL.Entities;
using UserManagementSystem.DAL.Repositories.Interfaces;

namespace UserManagementSystem.DAL.Repositories
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>
          where TEntity : BaseEntity
    {
        protected readonly UserManagementSystemContext _dbContext;

        public BaseRepository(UserManagementSystemContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> items = _dbContext.Set<TEntity>();

            if (filter != null)
            {
                items = items.Where(filter);
            }

            return items;
        }

        public TEntity GetEntity(Expression<Func<TEntity, bool>> filter)
        {
            return _dbContext.Set<TEntity>().FirstOrDefault(filter);
        }

        public void Insert(TEntity entityInput)
        {
            _dbContext.Set<TEntity>().Add(entityInput);
        }

        public void Update(TEntity entity)
        {
            _dbContext.Entry(entity).State = _dbContext.Entry(entity).State != EntityState.Modified ?
                EntityState.Modified : _dbContext.Entry(entity).State;
        }

        public bool Save()
        {
            return _dbContext.SaveChanges() > 0;
        }

        public bool Delete(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
            return _dbContext.SaveChanges() > 0;
        }

        public async Task<bool> DoesEntityExistAsync(Guid id)
        {
            return await _dbContext.Set<TEntity>().AnyAsync(m => m.Id == id);
        }
    }
}
