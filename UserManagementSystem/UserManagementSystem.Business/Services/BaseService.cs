using AutoMapper;
using UserManagementSystem.Business.Interfaces;
using UserManagementSystem.Business.Models;
using UserManagementSystem.DAL.Entities;
using UserManagementSystem.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace UserManagementSystem.Business.Services
{
    public class BaseService<TEntity, TResponseModel, TModel> : IBaseService<TEntity, TResponseModel, TModel>
        where TEntity : BaseEntity
        where TResponseModel : BaseResponseModel
        where TModel : BaseModel
    {
        private readonly IBaseRepository<TEntity> _repository;
        private readonly IMapper _mapper;

        protected IValidationDictionary _validationDictionary;

        public IValidationDictionary ValidationDictionary
        {
            get { return _validationDictionary; }
            set { _validationDictionary = value; }
        }

        public BaseService(IBaseRepository<TEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public virtual IQueryable<TResponseModel> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            var entities = _repository.GetAll(filter);

            return entities.Select(m => _mapper.Map<TEntity, TResponseModel>(m));
        }

        public TResponseModel GetResponseModel(Expression<Func<TEntity, bool>> filter = null)
        {
            var entity = _repository.GetEntity(filter);
            return _mapper.Map<TEntity, TResponseModel>(entity);
        }

        public virtual TEntity OnBeforeInsert(TModel model)
        {
            TEntity entity = _mapper.Map<TEntity>(model);

            return entity;
        }

        public virtual TResponseModel OnAfterInsert(TEntity entity)
        {
            return _mapper.Map<TResponseModel>(entity);
        }

        public TResponseModel Insert(TModel model)
        {
            var entity = OnBeforeInsert(model);

            _repository.Insert(entity);

            return OnAfterInsert(entity);
        }

        public virtual TEntity OnBeforeUpdate(Guid id, TModel model)
        {
            var entity = _repository.GetEntity(x => x.Id == id);

            _mapper.Map(model, entity);

            return entity;
        }

        public virtual TResponseModel Update(Guid id, TModel model)
        {
            var entity = OnBeforeUpdate(id, model);

            _repository.Update(entity);

            return OnAfterUpdate(entity);
        }

        public virtual TResponseModel OnAfterUpdate(TEntity entity)
        {
            return _mapper.Map<TResponseModel>(entity);
        }

        public virtual bool Save()
        {
            return _repository.Save();
        }

        public virtual bool Delete(Guid id)
        {
            var entity = _repository.GetEntity(e => e.Id == id);
            if (entity == null)
            {
                return false;
            }

            return _repository.Delete(entity);
        }

        public virtual async Task<bool> DoesEntityExistAsync(Guid id)
        {
            return await _repository.DoesEntityExistAsync(id);
        }
    }
}
