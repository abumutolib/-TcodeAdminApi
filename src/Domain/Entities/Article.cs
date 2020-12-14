using System.Collections.Generic;
using Domain.Common;

namespace Domain.Entities
{
    public class Article : AuditableEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string HtmlContent { get; set; }
        public bool IsActive { get; set; }
        public int UserId { get; set; }
        public virtual AppDataUser User { get; set; }
        public virtual ICollection<ArticleImage> Images { get; set; }
        public virtual ICollection<ArticleComment> Comments { get; set; }
    }
}
