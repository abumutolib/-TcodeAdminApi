using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Application.Common.Models;
using Application.Common.Interfaces;

namespace Application.GroupLanguageTools.Queries
{
    public class SelectListGLTQuery : IRequest<SelectListVm>
    {
    }

    internal class SelectListGLTQueryHandler : IRequestHandler<SelectListGLTQuery, SelectListVm>
    {
        private readonly IAppDbContext _context;

        public SelectListGLTQueryHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<SelectListVm> Handle(SelectListGLTQuery request, CancellationToken cancellationToken)
        {
            var vm = new SelectListVm();
            await _context.GroupLanguageTools.Where(x => x.IsActive).ForEachAsync(x => 
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
