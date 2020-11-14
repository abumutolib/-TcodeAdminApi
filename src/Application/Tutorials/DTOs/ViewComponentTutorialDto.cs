using System.Collections.Generic;
using Domain.Entities;
using Application.Common.Mappings;

namespace Application.Tutorials.DTOs
{
    public class ViewComponentTutorialDto : IMapFrom<Tutorial>
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }

    public class ViewComponentTutorialsVm
    {
        public string Title { get; set; }
        public IList<ViewComponentTutorialDto> List { get; set; }
    }
}
