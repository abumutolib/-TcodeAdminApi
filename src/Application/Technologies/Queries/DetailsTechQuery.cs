using MediatR;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Technologies.DTOs;

namespace Application.Technologies.Queries
{
    public class DetailsTechQuery : IRequest<TechnologyDto>
    {
        public int Id { get; set; }
    }

    internal class DetailsTechQueryHandler : IRequestHandler<DetailsTechQuery, TechnologyDto>
    {
        private readonly IMapper _mapper;
        private readonly IAppDbContext _context;

        public DetailsTechQueryHandler(IMapper mapper, IAppDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<TechnologyDto> Handle(DetailsTechQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Technologies.FindAsync(request.Id);
            if (entity == null)
                throw new NotFoundException(nameof(Technology), request.Id);
            return _mapper.Map<TechnologyDto>(entity);
        }
    }
}
