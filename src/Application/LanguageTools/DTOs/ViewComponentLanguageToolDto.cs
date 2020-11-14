using AutoMapper;
using System.Collections.Generic;
using Domain.Entities;
using Application.Common.Mappings;

namespace Application.LanguageTools.DTOs
{
    public class ViewComponentLanguageToolDto : IMapFrom<LanguageTool>
    {
        public int Id { get; set; }
        public int TechId { get; set; }
        public string Title { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<LanguageTool, ViewComponentLanguageToolDto>()
                .ForMember(d => d.TechId, opt => opt.MapFrom(s => s.TechnologyId));
        }
    }

    public class ViewComponentLanguageToolVm
    {
        public IList<ViewComponentLanguageToolDto> List { get; set; }
    }
}
