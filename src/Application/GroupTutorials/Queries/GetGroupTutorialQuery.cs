using MediatR;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Application.Common.Interfaces;
using AutoMapper.QueryableExtensions;
using Application.GroupTutorials.DTOs;

namespace Application.GroupTutorials.Queries
{
    public class GetGroupTutorialQuery : IRequest<GroupTutorialVm>
    {
    }
    internal class GetGroupTutorialQueryHandler : IRequestHandler<GetGroupTutorialQuery, GroupTutorialVm>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public GetGroupTutorialQueryHandler(IAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GroupTutorialVm> Handle(GetGroupTutorialQuery request, CancellationToken cancellationToken)
        {
            var vm = new GroupTutorialVm();
            vm.List = await _context.GroupTutorials
                            .ProjectTo<GroupTutorialDto>(_mapper.ConfigurationProvider)
                            .ToListAsync(cancellationToken);
            return vm;
        }
    }
}
