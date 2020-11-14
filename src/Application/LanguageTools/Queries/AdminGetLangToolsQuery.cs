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
    public class AdminGetLangToolsQuery : IRequest<LanguageToolVm>
    {
    }

    internal class AdminGetLangToolsQueryHandler : IRequestHandler<AdminGetLangToolsQuery, LanguageToolVm>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public AdminGetLangToolsQueryHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<LanguageToolVm> Handle(AdminGetLangToolsQuery request, CancellationToken cancellationToken)
        {
            var vm = new LanguageToolVm();
            vm.List = await _context.LanguageTools
                            .ProjectTo<LanguageToolDto>(_mapper.ConfigurationProvider)
                            .ToListAsync(cancellationToken);
            return vm;
        }
    }
}
