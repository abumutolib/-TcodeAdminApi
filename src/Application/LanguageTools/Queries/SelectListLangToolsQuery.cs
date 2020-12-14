using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Models;
using Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Application.LanguageTools.Queries
{
    public class SelectListLangToolsQuery : IRequest<SelectListVm>
    {
    }

    internal class SelectListLangToolsQueryHandler : IRequestHandler<SelectListLangToolsQuery, SelectListVm>
    {
        private readonly IAppDbContext _context;

        public SelectListLangToolsQueryHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<SelectListVm> Handle(SelectListLangToolsQuery request, CancellationToken cancellationToken)
        {
            var vm = new SelectListVm();
            await _context.LanguageTools.Where(x => x.IsActive).ForEachAsync(x =>
            {
                vm.List.Add(new SelectListDto
                {
                    Id = x.Id,
                    Title = x.Title
                });
            });
            return vm;
        }
    }
}
