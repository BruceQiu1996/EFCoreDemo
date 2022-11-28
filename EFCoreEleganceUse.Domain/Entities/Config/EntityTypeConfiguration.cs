using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreEleganceUse.Domain.Entities.Config
{
    public abstract class EntityTypeConfiguration<TEntity, TKey> : IEntityTypeConfiguration<TEntity>
       where TEntity : AggregateRoot<TKey>
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            var entityType = typeof(TEntity);

            builder.HasKey(x => x.Id);

            if (typeof(ISoftDelete).IsAssignableFrom(entityType))
            {
                builder.HasQueryFilter(d => EF.Property<bool>(d, "IsDeleted") == false);
            }
        }
    }
}
