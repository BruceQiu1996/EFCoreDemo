using EFCoreEleganceUse.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCoreEleganceUse.EF.Mysql
{
    /// <summary>
    /// 图书馆数据库上下文
    /// </summary>
    public class LibraryDbContext : DbContext
    {
        private readonly EFEntityInfo _efEntitysInfo;

        public LibraryDbContext(DbContextOptions options, EFEntityInfo efEntityInfo) : base(options)
        {
            _efEntitysInfo = efEntityInfo;
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("utf8mb4 ");
            var (Assembly, Types) = _efEntitysInfo.EFEntitiesInfo;
            foreach (var entityType in Types)
            {
                modelBuilder.Entity(entityType);
            }
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
