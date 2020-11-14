using MediatR;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using Application.Common.Interfaces;
using Application.GroupTutorials.DTOs;
using Application.Common.Exceptions;
using Domain.Entities;

namespace Application.GroupTutorials.Queries
{
    public class DetailsGroupTutorialQuery : IRequest<GroupTutorialDto>
    {
        public int Id { get; set; }
    }

    internal class DetailsGroupTutorialQueryHandler : IRequestHandler<DetailsGroupTutorialQuery, GroupTutorialDto>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public DetailsGroupTutorialQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<GroupTutorialDto> Handle(DetailsGroupTutorialQuery request, CancellationToken cancellationToken)
        {
            var groupTutorial = await _context.GroupTutorials.FindAsync(request.Id);
            if (groupTutorial == null)
                throw new NotFoundException(nameof(GroupTutorial), request.Id);

            var output = _mapper.Map<GroupTutorialDto>(groupTutorial);

            return output;
        }
    }
}
