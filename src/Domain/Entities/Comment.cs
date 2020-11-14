using Domain.Common;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Comment : AuditableEntity
    {
        public int Id { get; set; }
        public string Message { get; set; }

        public virtual ICollection<ArticleComment> Articles { get; set; }
    }
}
