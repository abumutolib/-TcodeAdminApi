using System.Collections.Generic;
using Domain.Common;

namespace Domain.Entities
{
    public class GroupTutorial : AuditableEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<Tutorial> Tutorials { get; set; }
    }
}
