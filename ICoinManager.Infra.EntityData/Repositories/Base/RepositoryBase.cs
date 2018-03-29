using ICoinManager.Domain.Base.Entities;
using ICoinManager.Domain.Contracts.Repositories.Base;
using ICoinManager.Infra.EntityData.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ICoinManager.Infra.EntityData.Repositories.Base
{
    public abstract class RepositoryBase<T> : IDisposable, IRepositoryBase<T> where T : BaseEntity
    {
        #region Constructor

        protected RepositoryBase(ICoinManagerContext context)
        {
            Context = context;
        }

        #endregion

        #region Properties

        protected ICoinManagerContext Context { get; private set; }

        #endregion

        #region Methods

        public IEnumerable<T> GetAll()
        {
            var entities = Context.Set<T>().AsNoTracking().ToList();
            return entities;
        }

        public T Get(Guid id)
        {
            var entity = Context.Set<T>().Find(id);
            return entity;
        }

        public IEnumerable<T> Find(Func<T, bool> predicate)
        {
            var entities = Context.Set<T>().Where(predicate).ToList();
            return entities;
        }


        public void Add(T entity)
        {
            Context.Set<T>().Add(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            Context.Set<T>().AddRange(entities);
        }


        public void Update(T entity)
        {
            var entityEntry = Context.Entry(entity);
            entityEntry.State = EntityState.Modified;
        }

        public void SaveOrAdd(T entity)
        {
            var ID = Guid.Empty;

            var propertyID = entity.GetType().GetProperty("Id");
            if (propertyID != null)
                ID = (Guid)propertyID.GetValue(entity);

            var obj = Get(ID);

            if (obj == null || Guid.Empty == (Guid)obj.GetType().GetProperty("Id").GetValue(entity))
                Add(entity);
            else
                Update(entity);
        }

        public void Remove(T entity)
        {
            Context.Set<T>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            Context.Set<T>().RemoveRange(entities);
        }


        public void Dispose()
        {
            Context.Dispose();
        }

        #endregion
    }
}