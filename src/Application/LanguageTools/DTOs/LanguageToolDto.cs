using AutoMapper;
using System.Collections.Generic;
using Domain.Entities;
using Application.Common.Mappings;
using Application.Common.Interfaces;

namespace Application.LanguageTools.DTOs
{
    public class LanguageToolDto : IMapFrom2<LanguageTool>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImagePath { get; set; }
        public bool IsActive { get; set; }
        public string Description { get; set; }
        public string Technology { get; set; }
        public int TechId { get; set; }
        public string Group { get; set; }

        public void Mapping(Profile profile, IPathProvider pathProvider)
        {
            profile.CreateMap<LanguageTool, LanguageToolDto>()
                .ForMember(d => d.Group, opt => opt.MapFrom(s => s.Group.Title))
                .ForMember(d => d.TechId, opt => opt.MapFrom(s => s.TechnologyId))
                .ForMember(d => d.Technology, opt => opt.MapFrom(s => s.Technology.Title))
                .ForMember(d => d.ImagePath, opt => opt.MapFrom(s => pathProvider.MapPath(s.ImagePath)));
        }
    }

    public class LanguageToolVm
    {
        public IList<LanguageToolDto> List { get; set; }
    }
}
