using System.Collections.Generic;
using Domain.Common;

namespace Domain.Entities
{
    public class Tutorial : AuditableEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsActive { get; set; }
        public string HtmlContent { get; set; }
        public int GroupId { get; set; }
        public int LanguageToolId { get; set; }
        public virtual GroupTutorial Group { get; set; }
        public virtual LanguageTool LanguageTool { get; set; }
        public ICollection<TutorialImage> TutorialImages { get; set; }
    }
}
