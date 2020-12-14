using Domain.Common;

namespace Domain.Entities
{
    public class Project : AuditableEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int UserId { get; set; }
        public virtual AppDataUser User { get; set; }
    }
}
