using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using DAL.DbContext;
using DAL.Interfaces;

namespace DAL.Implementations
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        internal ApplicationDbContext Context;
        internal DbSet<TEntity> DBSet;

        public GenericRepository(ApplicationDbContext context)
        {
            Context = context;
            DBSet = context.Set<TEntity>();
        }

        public virtual IQueryable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = DBSet;
            if (filter != null) query = query.Where(filter);
            query = includeProperties.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries).Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            return orderBy != null ? orderBy(query) : query;
        }

        public virtual TEntity GetById(object id)
        {
            return DBSet.Find(id);
        }

        public virtual void Insert(TEntity entity)
        {
            DBSet.Add(entity);
        }

        public virtual void Delete(object id)
        {
            var entityToDelete = DBSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (Context.Entry(entityToDelete).State == EntityState.Detached) DBSet.Attach(entityToDelete);
            DBSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            DBSet.Attach(entityToUpdate);
            Context.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}