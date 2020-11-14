using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.LanguageTools.DTOs;

namespace Application.LanguageTools.Queries
{
    public class SelectedLangToolQuery : IRequest<SelectedLangToolDto>
    {
        public int TechId { get; set; }
        public int LangToolId { get; set; }
    }

    internal class SelectedLangToolQueryHanler : IRequestHandler<SelectedLangToolQuery, SelectedLangToolDto>
    {
        private readonly IApplicationDbContext _context;

        public SelectedLangToolQueryHanler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<SelectedLangToolDto> Handle(SelectedLangToolQuery request, CancellationToken cancellationToken)
        {
            if (request.LangToolId != 0)
            {
                return new SelectedLangToolDto { TechId = request.TechId, LangToolId = request.LangToolId };
            }

            var tech = await _context.Technologies.FindAsync(request.TechId);
            if (tech == null)
            {
                throw new NotFoundException(nameof(Technology), request.TechId);
            }
            var tool = await _context.LanguageTools.FirstOrDefaultAsync(x => x.TechnologyId == tech.Id);
            if (tool == null)
            {
                throw new NotFoundException(nameof(LanguageTool), request.TechId);
            }
            var vm = new SelectedLangToolDto
            {
                TechId = tech.Id,
                LangToolId = tool.Id
            };
            return vm;
        }
    }
}
