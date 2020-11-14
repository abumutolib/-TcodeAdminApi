using MediatR;
using AutoMapper;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using Application.Articles.DTOs;
using Application.Common.Models;
using Application.Common.Interfaces;
using Application.Common.Pagination;

namespace Application.Articles.Queries
{
    public class GetArticlesQuery : IRequest<PagedResult<ArticleListDto>>
    {
        public string Title { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
    internal class GetArticlesQueryHandler : IRequestHandler<GetArticlesQuery, PagedResult<ArticleListDto>>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public GetArticlesQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<PagedResult<ArticleListDto>> Handle(GetArticlesQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Article> articles = _context.Articles;

            if (!string.IsNullOrEmpty(request.Title) && request.Title.Length >= 3)
                articles = articles.Where(x => x.Title.ToLower().Contains(request.Title.ToLower()));

            PagedResult<ArticleListDto> result = await articles.OrderByDescending(x => x.Created)
                                                               .ProjectTo<ArticleListDto>(_mapper.ConfigurationProvider)
                                                               .GetPagedAsync(request.Page, request.PageSize, cancellationToken);
            return result;
        }
    }
}
