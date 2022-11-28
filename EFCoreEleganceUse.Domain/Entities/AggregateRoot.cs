namespace EFCoreEleganceUse.Domain.Entities
{
    public abstract class AggregateRoot<TKey> : IEFEntity<TKey>
    {
        public TKey Id { get; set; }
    }
}
