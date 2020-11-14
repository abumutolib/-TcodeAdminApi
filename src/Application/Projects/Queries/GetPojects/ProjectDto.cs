using System;
using AutoMapper;
using Domain.Entities;
using Application.Common.Mappings;

namespace Application.Projects.Queries.GetPojects
{
    public class ProjectDto : IMapFrom<Project>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime PublishDate { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Project, ProjectDto>()
                .ForMember(d => d.PublishDate, opt => opt.MapFrom(s => s.Created));
        }
    }
}
