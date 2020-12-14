using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Application.Common.Models;
using Application.Common.Interfaces;

namespace Application.GroupTutorials.Queries
{
    public class SelectListGroupTutorialQuery : IRequest<SelectListVm>
    {
    }

    internal class SelectListGroupTutorialQueryHandler : IRequestHandler<SelectListGroupTutorialQuery, SelectListVm>
    {
        private readonly IAppDbContext _context;

        public SelectListGroupTutorialQueryHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<SelectListVm> Handle(SelectListGroupTutorialQuery request, CancellationToken cancellationToken)
        {
            var vm = new SelectListVm();
            await _context.GroupTutorials.Where(x => x.IsActive).ForEachAsync(x =>
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
