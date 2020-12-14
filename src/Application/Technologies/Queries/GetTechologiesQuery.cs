using MediatR;
using AutoMapper;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using Application.Common.Interfaces;
using Application.Technologies.DTOs;

namespace Application.Technologies.Queries
{
    public class GetTechologiesQuery : IRequest<TechnologyVm>
    {
    }

    internal class GetTechnologiesQueryHandler : IRequestHandler<GetTechologiesQuery, TechnologyVm>
    {
        private readonly IMapper _mapper;
        private readonly IAppDbContext _context;

        public GetTechnologiesQueryHandler(IMapper mapper, IAppDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<TechnologyVm> Handle(GetTechologiesQuery request, CancellationToken cancellationToken)
        {
            var vm = new TechnologyVm();
            vm.List = await _context.Technologies
                            .Where(x => x.LanguageTools.Any())
                            .ProjectTo<TechnologyDto>(_mapper.ConfigurationProvider)
                            .ToListAsync(cancellationToken);
            return vm;
        }
    }
}
