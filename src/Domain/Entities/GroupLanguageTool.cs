using System.Collections.Generic;
using Domain.Common;

namespace Domain.Entities
{
    public class GroupLanguageTool : AuditableEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<LanguageTool> LanguageTools { get; set; }
    }
}
