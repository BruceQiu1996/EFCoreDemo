using EFCoreEleganceUse.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EFCoreEleganceUse.EF.Mysql.Repository
{
    public class GenericRepository<TEntity, Tkey> : IAsyncRepository<TEntity, Tkey> where TEntity : class
    {
        protected readonly LibraryDbContext _dbContext;

        public GenericRepository(LibraryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        ~GenericRepository()
        {
            _dbContext?.Dispose();
        }

        public virtual IQueryable<TEntity> All()
        {
            return All(null);
        }

        public virtual IQueryable<TEntity> All(string[] propertiesToInclude)
        {
            var query = _dbContext.Set<TEntity>().AsNoTracking();

            if (propertiesToInclude != null)
            {
                foreach (var property in propertiesToInclude.Where(p => !string.IsNullOrWhiteSpace(p)))
                {
                    query = query.Include(property);
                }
            }

            return query;
        }

        public virtual IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> filter)
        {
            return Where(filter, null);
        }

        public virtual IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> filter, string[] propertiesToInclude)
        {
            var query = _dbContext.Set<TEntity>().AsNoTracking();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (propertiesToInclude != null)
            {
                foreach (var property in propertiesToInclude.Where(p => !string.IsNullOrWhiteSpace(p)))
                {
                    query = query.Include(property);
                }
            }

            return query;
        }
    }
}
