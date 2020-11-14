using AutoMapper;
using System.Collections.Generic;
using Domain.Entities;
using Application.Common.Mappings;
using Application.Common.Interfaces;

namespace Application.Technologies.DTOs
{
    public class TechnologyDto : IMapFrom2<Technology>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public bool IsActive { get; set; }
        public string ImagePath { get; set; }

        public void Mapping(Profile profile, IPathProvider pathProvider)
        {
            profile.CreateMap<Technology, TechnologyDto>()
                   .ForMember(d => d.ImagePath, opt => opt.MapFrom(s => pathProvider.MapPath(s.ImagePath)));
        }
    }

    public class TechnologyVm
    {
        public IList<TechnologyDto> List { get; set; }
    }
}
