using AutoMapper;
using System.Linq;
using System.Collections.Generic;
using Domain.Entities;
using Application.Common.Mappings;

namespace Application.Tutorials.DTOs
{
    public class TutorialDto : IMapFrom<Tutorial>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImagePath { get; set; }
        public string LanguageTool { get; set; }
        public string Group { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Tutorial, TutorialDto>()
                   .ForMember(d => d.Group, opt => opt.MapFrom(s => s.Group.Title))
                   .ForMember(d => d.LanguageTool, opt => opt.MapFrom(s => s.LanguageTool.Title))
                   .ForMember(d => d.ImagePath, opt => opt.MapFrom(s => s.TutorialImages.Where(x => x.IsPrimary).FirstOrDefault().Path));
        }
    }

    public class TutorialVm
    {
        public IList<TutorialDto> List { get; set; }
    }
}
