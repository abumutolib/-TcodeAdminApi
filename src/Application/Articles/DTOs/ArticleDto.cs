using System;
using AutoMapper;
using System.Linq;
using Domain.Entities;
using Application.Common.Mappings;

namespace Application.Articles.DTOs
{
    public class ArticleDto : IMapFrom<Article>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Article, ArticleDto>()
                   .ForMember(d => d.Content, opt => opt.MapFrom(s => s.HtmlContent));
        }
    }

    public class ArticleListDto : IMapFrom<Article>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Author { get; set; }
        public DateTime PublishDate { get; set; }
        public string Description { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Article, ArticleListDto>()
                .ForMember(d => d.PublishDate, opt => opt.MapFrom(s => s.Created))
                .ForMember(d => d.Image, opt => opt.MapFrom(s => s.Images.Where(x => x.IsPrimary).FirstOrDefault().Path))
                .ForMember(d => d.Author, opt => opt.MapFrom(s => s.User.FirstName + " " + s.User.LastName + " " + s.User.MiddleName));
        }
    }
}
