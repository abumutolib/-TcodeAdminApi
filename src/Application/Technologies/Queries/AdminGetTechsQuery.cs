using MediatR;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using Application.Common.Interfaces;
using Application.Technologies.DTOs;

namespace Application.Technologies.Queries
{
    public class AdminGetTechsQuery : IRequest<TechnologyVm>
    {
    }

    internal class AdminGetTechsQueryHandler : IRequestHandler<AdminGetTechsQuery, TechnologyVm>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public AdminGetTechsQueryHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<TechnologyVm> Handle(AdminGetTechsQuery request, CancellationToken cancellationToken)
        {
            var vm = new TechnologyVm();
            vm.List = await _context.Technologies
                            .ProjectTo<TechnologyDto>(_mapper.ConfigurationProvider)
                            .ToListAsync(cancellationToken);
            return vm;
        }
    }
}
