using MediatR;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using Application.Common.Interfaces;
using Application.LanguageTools.DTOs;

namespace Application.LanguageTools.Queries
{
    public class GetLanguageToolsQuery : IRequest<LanguageToolVm>
    {
    }

    internal class GetLanguageToolsQueryHandler : IRequestHandler<GetLanguageToolsQuery, LanguageToolVm>
    {
        private readonly IMapper _mapper;
        private readonly IAppDbContext _context;

        public GetLanguageToolsQueryHandler(IMapper mapper, IAppDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<LanguageToolVm> Handle(GetLanguageToolsQuery request, CancellationToken cancellationToken)
        {
            return new LanguageToolVm
            {
                List = await _context.LanguageTools
                            .ProjectTo<LanguageToolDto>(_mapper.ConfigurationProvider)
                            .ToListAsync(cancellationToken)
            };
        }
    }
}
