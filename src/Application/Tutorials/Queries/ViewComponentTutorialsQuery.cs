using MediatR;
using AutoMapper;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using Application.Tutorials.DTOs;
using Application.Common.Interfaces;
using Application.Common.Exceptions;

namespace Application.Tutorials.Queries
{
    public class ViewComponentTutorialsQuery : IRequest<ViewComponentTutorialsVm>
    {
        public int LangToolId { get; set; }
    }

    public class ViewComponentTutorialsQueryHadler : IRequestHandler<ViewComponentTutorialsQuery, ViewComponentTutorialsVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ViewComponentTutorialsQueryHadler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ViewComponentTutorialsVm> Handle(ViewComponentTutorialsQuery request, CancellationToken cancellationToken)
        {
            var vm = new ViewComponentTutorialsVm();
            var langTool = await _context.LanguageTools.FindAsync(request.LangToolId);
            if (langTool == null)
            {
                throw new NotFoundException(nameof(LanguageTool), request.LangToolId);
            }
            vm.Title = langTool.Title;
            vm.List = await _context.Tutorials.Where(x => x.LanguageToolId == langTool.Id)
                                               .ProjectTo<ViewComponentTutorialDto>(_mapper.ConfigurationProvider)
                                               .ToListAsync(cancellationToken);
            return vm;
        }
    }
}
