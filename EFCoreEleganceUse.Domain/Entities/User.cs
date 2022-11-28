using System.ComponentModel.DataAnnotations;

namespace EFCoreEleganceUse.Domain.Entities
{
    public class User : AggregateRoot<string>
    {
        public string UserName { get; set; }
        public DateTime Birthday { get; set; }
        public virtual ICollection<Book> Books { get; set; } //该用户所借的书
    }
}
