using System.Collections.Generic;
using Domain.Common;

namespace Domain.Entities
{
    public class LanguageTool : AuditableEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsActive { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }
        public int GroupId { get; set; }
        public int TechnologyId { get; set; }
        public virtual Technology Technology { get; set; }
        public virtual GroupLanguageTool Group { get; set; }
        public virtual ICollection<Tutorial> Tutorials { get; set; }
    }
}
