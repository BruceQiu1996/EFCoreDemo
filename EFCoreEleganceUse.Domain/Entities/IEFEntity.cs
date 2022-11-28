namespace EFCoreEleganceUse.Domain.Entities
{
    public interface IEFEntity<TKey>
    {
        public TKey Id { get; set; }
    }
}
