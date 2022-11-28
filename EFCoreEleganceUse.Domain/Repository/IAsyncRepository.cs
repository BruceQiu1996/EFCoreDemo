using System.Linq.Expressions;

namespace EFCoreEleganceUse.Domain.Repository
{
    public interface IAsyncRepository<TEntity, Tkey> where TEntity : class
    {
        // query
        IQueryable<TEntity> All();
        IQueryable<TEntity> All(string[] propertiesToInclude);
        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> filter);
        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> filter, string[] propertiesToInclude);
    }
}
