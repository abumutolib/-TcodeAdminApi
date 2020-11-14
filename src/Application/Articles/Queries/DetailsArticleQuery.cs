using MediatR;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Application.Articles.DTOs;
using Application.Common.Interfaces;
using Application.Common.Exceptions;

namespace Application.Articles.Queries
{
    public class DetailsArticleQuery : IRequest<ArticleDto>
    {
        public int Id { get; set; }
    }

    public class DetailsArticleQueryHandler : IRequestHandler<DetailsArticleQuery, ArticleDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public DetailsArticleQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ArticleDto> Handle(DetailsArticleQuery request, CancellationToken cancellationToken)
        {
            var vm = new ArticleDto();

            Article article = await _context.Articles.FindAsync(request.Id);

            if (article == null)
                throw new NotFoundException(nameof(Article), request.Id);

            vm = _mapper.Map<ArticleDto>(article);

            return vm;
        }
    }
}
