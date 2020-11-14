using MediatR;
using AutoMapper;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using Application.Common.Interfaces;
using Application.Common.Exceptions;
using Application.LanguageTools.DTOs;

namespace Application.LanguageTools.Queries
{
    public class ViewComponentLanguageToolsQuery : IRequest<ViewComponentLanguageToolVm>
    {
        public int TechId { get; set; }
    }

    public class ViewComponentLanguageToolsQueryHandler : IRequestHandler<ViewComponentLanguageToolsQuery, ViewComponentLanguageToolVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ViewComponentLanguageToolsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ViewComponentLanguageToolVm> Handle(ViewComponentLanguageToolsQuery request, CancellationToken cancellationToken)
        {
            var vm = new ViewComponentLanguageToolVm();
            var tech = await _context.Technologies.FindAsync(request.TechId);
            if (tech == null)
            {
                throw new NotFoundException(nameof(Technology), request.TechId);
            }

            vm.List = await _context.LanguageTools.Where(x => x.TechnologyId == request.TechId && x.Tutorials.Any())
                                            .ProjectTo<ViewComponentLanguageToolDto>(_mapper.ConfigurationProvider)
                                            .ToListAsync(cancellationToken);

            return vm;
        }
    }
}
