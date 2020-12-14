using MediatR;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using Application.Common.Interfaces;
using Application.GroupLanguageTools.DTOs;

namespace Application.GroupLanguageTools.Queries
{
    public class GroupLangToolQuery : IRequest<GroupLangToolVm>
    {

    }

    internal class GroupLangToolQueryHandler : IRequestHandler<GroupLangToolQuery, GroupLangToolVm>
    {
        private readonly IMapper _mapper;
        private readonly IAppDbContext _context;

        public GroupLangToolQueryHandler(IMapper mapper, IAppDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<GroupLangToolVm> Handle(GroupLangToolQuery request, CancellationToken cancellationToken)
        {
            var vm = new GroupLangToolVm();

            vm.List = await _context.GroupLanguageTools
                              .ProjectTo<GroupLangToolDto>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
            return vm;
        }
    }
}
