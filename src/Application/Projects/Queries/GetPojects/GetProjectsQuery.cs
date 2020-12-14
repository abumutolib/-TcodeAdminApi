using MediatR;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using Application.Common.Interfaces;

namespace Application.Projects.Queries.GetPojects
{
    public class GetProjectsQuery : IRequest<ProjectVm>
    {
    }
    public class GetProjectsQueryHandler : IRequestHandler<GetProjectsQuery, ProjectVm>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public GetProjectsQueryHandler(IAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ProjectVm> Handle(GetProjectsQuery request, CancellationToken cancellationToken)
        {
            var vm = new ProjectVm();
            vm.List = await _context.Projects
                .ProjectTo<ProjectDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            return vm;
        }
    }
}
