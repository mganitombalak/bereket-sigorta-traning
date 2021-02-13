using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Bereket.Domain;

namespace Bereket.Repository.Interfaces{

    public interface IBaseCrudRepository<T, TId> : IDisposable
    {
        public BereketDbContext CurrentDbcontext { get; }

        ICollection<T> Find(Expression<Func<T, bool>> filter = null);

        T FindById(TId id, params string[] includes);

        T Create(T entity);

        T Update(T entity, params string[] excludingFields);

        void UpdateStatusById(TId id, bool status);
        void BatchUpdateStatusById(IEnumerable<T> entities, bool status);

        void Delete(T entity);

        void DeleteById(TId id);

        void SoftDelete(TId id);

        void BatchDelete(IEnumerable<T> entities);
        void BatchSoftDelete(IEnumerable<T> entities);
        bool Exist(TId id);

        bool Exist(Expression<Func<T, bool>> expression);

        int GetTotalRecordCount();

        bool Truncate();
    }
}