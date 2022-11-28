using EFCoreEleganceUse.Domain.Repository;

namespace EFCoreEleganceUse.EF.Mysql.Repository
{
    public class WorkUnit : IWorkUnit, IDisposable
    {
        private readonly LibraryDbContext _dbContext;
        public WorkUnit(LibraryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _dbContext.Dispose();
            }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool _disposed = false;
        ~WorkUnit()
        {
            Dispose();
        }
    }
}
