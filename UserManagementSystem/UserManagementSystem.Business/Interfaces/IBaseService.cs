using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using UserManagementSystem.Business.Models;
using UserManagementSystem.DAL.Entities;

namespace UserManagementSystem.Business.Interfaces
{
    public interface IBaseService<TEntity, TResponseModel, TModel>
        where TEntity : BaseEntity
        where TResponseModel : BaseResponseModel
        where TModel : BaseModel
    {
        IQueryable<TResponseModel> GetAll(Expression<Func<TEntity, bool>> filter = null);
        bool Delete(Guid id);
        TResponseModel GetResponseModel(Expression<Func<TEntity, bool>> filter = null);

        bool Save();

        TEntity OnBeforeInsert(TModel model);

        TResponseModel OnAfterInsert(TEntity entity);

        TResponseModel Insert(TModel model);

        TEntity OnBeforeUpdate(Guid id, TModel model);

        TResponseModel OnAfterUpdate(TEntity entity);

        TResponseModel Update(Guid id, TModel model);

        Task<bool> DoesEntityExistAsync(Guid id);

        IValidationDictionary ValidationDictionary { get; set; }
    }
}
