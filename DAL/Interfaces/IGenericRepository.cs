using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");
        TEntity GetById(object id);
        Task<TEntity> GetByIdAsync(object id);

        void Insert(TEntity entity);
        
        void Delete(object id);
        Task DeleteAsync(object id);
        void Delete(TEntity entityToDelete);
        
        void Update(TEntity entityToUpdate);
    }
}