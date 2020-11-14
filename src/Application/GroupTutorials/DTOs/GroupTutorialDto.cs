using Domain.Entities;
using Application.Common.Mappings;

namespace Application.GroupTutorials.DTOs
{
    public class GroupTutorialDto : IMapFrom<GroupTutorial>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsActive { get; set; }
    }
}
