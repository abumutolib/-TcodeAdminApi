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
    public class GetGLTQuery : IRequest<GroupLangToolVm>
    {

    }

    internal class GetGLTQueryHandler : IRequestHandler<GetGLTQuery, GroupLangToolVm>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public GetGLTQueryHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<GroupLangToolVm> Handle(GetGLTQuery request, CancellationToken cancellationToken)
        {
            var vm = new GroupLangToolVm();

            vm.List = await _context.GroupLanguageTools
                              .ProjectTo<GroupLangToolDto>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
            return vm;
        }
    }
}
