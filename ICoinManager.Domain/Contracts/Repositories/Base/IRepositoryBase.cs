using System;
using System.Collections.Generic;

namespace ICoinManager.Domain.Contracts.Repositories.Base
{
    public interface IRepositoryBase<T> where T : class
    {
        //Create
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);

        //Read
        T Get(Guid id);
        IEnumerable<T> Find(Func<T, bool> predicate);
        IEnumerable<T> GetAll();

        //Update
        void Update(T entity);

        //Delete
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);

        //Create/Add
        void SaveOrAdd(T entity);
    }
}