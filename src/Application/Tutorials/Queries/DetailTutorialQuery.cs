using MediatR;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using Application.Tutorials.DTOs;
using Application.Common.Interfaces;

namespace Application.Tutorials.Queries
{
    public class DetailTutorialQuery : IRequest<DetailTutorialVm>
    {
        public int Id { get; set; }
    }

    public class DetailTutorialQueryHandler : IRequestHandler<DetailTutorialQuery, DetailTutorialVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public DetailTutorialQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<DetailTutorialVm> Handle(DetailTutorialQuery request, CancellationToken cancellationToken)
        {
            var vm = _mapper.Map<DetailTutorialVm>(await _context.Tutorials.FindAsync(request.Id));
            return vm;
        }
    }
}
