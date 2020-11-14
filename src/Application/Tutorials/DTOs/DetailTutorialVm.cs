using Domain.Entities;
using Application.Common.Mappings;

namespace Application.Tutorials.DTOs
{
    public class DetailTutorialVm : IMapFrom<Tutorial>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string HtmlContent { get; set; }
    }
}
