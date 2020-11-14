using MediatR;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Application.Common.Interfaces;
using Application.LanguageTools.DTOs;
using Application.Common.Exceptions;

namespace Application.LanguageTools.Queries
{
    public class DetailsLangToolQuery : IRequest<LanguageToolDto>
    {
        public int Id { get; set; }
    }

    internal class DetailsLangToolQueryHandler : IRequestHandler<DetailsLangToolQuery, LanguageToolDto>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public DetailsLangToolQueryHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<LanguageToolDto> Handle(DetailsLangToolQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.LanguageTools.FindAsync(request.Id);
            if (entity == null)
                throw new NotFoundException(nameof(LanguageTool), request.Id);

            return _mapper.Map<LanguageToolDto>(entity);
        }
    }
}
