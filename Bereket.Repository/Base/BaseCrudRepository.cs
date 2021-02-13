using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Bereket.Domain;
using Bereket.Domain.Interfaces;
using Bereket.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Bereket.Repository.Base{

    public abstract class BaseCrudRepository<T,TId>: IBaseCrudRepository<T, TId> where T:class,IBaseEntity<TId>{

        protected BaseCrudRepository(BereketDbContext dbContext)
        {
            CurrentDbcontext = dbContext;
        }

        public BereketDbContext CurrentDbcontext { get; }


        public virtual ICollection<T> Find(Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> queryable = CurrentDbcontext.Set<T>();
            if (filter != null) queryable = queryable.Where(filter);
            return queryable.AsNoTracking().ToList();
        }


        public T FindById(TId id, params string[] includes)
        {
            IQueryable<T> queryable = CurrentDbcontext.Set<T>();
            if (includes.Any()) includes.ToList().ForEach(i => { queryable = queryable.Include(i); });
            return queryable.FirstOrDefault(e => e.Id.Equals(id));
        }

        public T Create(T entity)
        {
            CurrentDbcontext.Set<T>().Add(entity);
            CurrentDbcontext.SaveChanges();
            return entity;
        }

        public virtual T Update(T entity, params string[] excludingFields)
        {
            CurrentDbcontext.Set<T>().Update(entity);
            CurrentDbcontext.SaveChanges();
            return entity;
        }

        public void UpdateStatusById(TId id, bool status)
        {
            var entity = FindById(id);

            CurrentDbcontext.Set<T>().Update(entity);
            CurrentDbcontext.SaveChanges();
        }

        public void BatchUpdateStatusById(IEnumerable<T> entities, bool status)
        {
            CurrentDbcontext.Set<T>().UpdateRange(entities);
            CurrentDbcontext.SaveChanges();
        }

        public void Delete(T entity)
        {
            CurrentDbcontext.Set<T>().Remove(entity);
            CurrentDbcontext.SaveChanges();
        }

        public void DeleteById(TId id)
        {
            CurrentDbcontext.Set<T>().Remove(FindById(id));
            CurrentDbcontext.SaveChanges();
        }

        public void SoftDelete(TId id)
        {
            var item = FindById(id);

            CurrentDbcontext.Set<T>().Update(item);
            CurrentDbcontext.SaveChanges();
        }

        public void BatchDelete(IEnumerable<T> entities)
        {
            if (entities == null) return;
            CurrentDbcontext.Set<T>().RemoveRange(entities);
            CurrentDbcontext.SaveChanges();
        }

        public void BatchSoftDelete(IEnumerable<T> entities)
        {
            if (entities == null) return;

            CurrentDbcontext.Set<T>().UpdateRange(entities);
            CurrentDbcontext.SaveChanges();
        }

        public bool Exist(TId id)
        {
            return CurrentDbcontext.Set<T>().Any(e => e.Id.Equals(id));
        }

        public bool Exist(Expression<Func<T, bool>> predicate)
        {
            return CurrentDbcontext.Set<T>().Any(predicate);
        }

        public int GetTotalRecordCount()
        {
            return CurrentDbcontext.Set<T>().Count();
        }
        
        public bool Truncate()
        {
            CurrentDbcontext.Database.ExecuteSqlRaw($"TRUNCATE TABLE \"{CurrentDbcontext.Set<T>().EntityType.GetTableName()}\" restart identity cascade");
            return true;
        }
        
        public void Dispose()
        {
            CurrentDbcontext.Dispose();
        }
    }  
}