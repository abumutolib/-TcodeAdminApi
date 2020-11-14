using Domain.Entities;
using Application.Common.Mappings;

namespace Application.GroupLanguageTools.DTOs
{
    public class GroupLangToolDto : IMapFrom<GroupLanguageTool>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsActive { get; set; }
    }
}
