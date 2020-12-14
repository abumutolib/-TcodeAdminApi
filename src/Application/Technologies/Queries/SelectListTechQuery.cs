using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Application.Common.Models;
using Application.Common.Interfaces;

namespace Application.Technologies.Queries
{
    public class SelectListTechQuery : IRequest<SelectListVm>
    {
    }

    internal class SelectListTechQueryHandler : IRequestHandler<SelectListTechQuery, SelectListVm>
    {
        private readonly IAppDbContext _context;

        public SelectListTechQueryHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<SelectListVm> Handle(SelectListTechQuery request, CancellationToken cancellationToken)
        {
            var vm = new SelectListVm();
            await _context.Technologies.Where(x => x.IsActive).ForEachAsync(x =>
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
