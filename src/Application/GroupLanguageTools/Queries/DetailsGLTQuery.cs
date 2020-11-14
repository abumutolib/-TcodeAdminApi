using MediatR;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Application.Common.Interfaces;
using Application.Common.Exceptions;
using Application.GroupLanguageTools.DTOs;

namespace Application.GroupLanguageTools.Queries
{
    public class DetailsGLTQuery : IRequest<GroupLangToolDto>
    {
        public int Id { get; set; }
    }

    internal class DetailsGLTQueryHandler : IRequestHandler<DetailsGLTQuery, GroupLangToolDto>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public DetailsGLTQueryHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<GroupLangToolDto> Handle(DetailsGLTQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.GroupLanguageTools.FindAsync(request.Id);
            if (entity == null)
                throw new NotFoundException(nameof(GroupLanguageTool), request.Id);

            var output = _mapper.Map<GroupLangToolDto>(entity);

            return output;
        }
    }
}
