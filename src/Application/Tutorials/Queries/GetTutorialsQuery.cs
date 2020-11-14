using MediatR;
using AutoMapper;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using Application.Tutorials.DTOs;
using Application.Common.Interfaces;

namespace Application.Tutorials.Queries
{
    public class GetTutorialsQuery : IRequest<TutorialVm>
    {
    }

    public class GetTutorialsQueryHandler : IRequestHandler<GetTutorialsQuery, TutorialVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetTutorialsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TutorialVm> Handle(GetTutorialsQuery request, CancellationToken cancellationToken)
        {
            var vm = new TutorialVm();

            vm.List = await _context.Tutorials
                            .Include(x => x.LanguageTool)
                            .Include(x => x.TutorialImages.Where(x => x.IsPrimary))
                            .ProjectTo<TutorialDto>(_mapper.ConfigurationProvider)
                            .ToListAsync(cancellationToken);
            return vm;
        }
    }
}
