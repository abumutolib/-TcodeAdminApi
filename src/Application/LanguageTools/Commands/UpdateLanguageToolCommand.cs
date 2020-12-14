using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Application.Common.Exceptions;
using Application.Common.Interfaces;

namespace Application.LanguageTools.Commands
{
    public class UpdateLanguageToolCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsActive { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }
        public int TechnologyId { get; set; }
        public int GroupId { get; set; }
    }

    public class UpdateLanguageToolCommandHandler : IRequestHandler<UpdateLanguageToolCommand, int>
    {
        private readonly IAppDbContext _context;

        public UpdateLanguageToolCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(UpdateLanguageToolCommand request, CancellationToken cancellationToken)
        {
            var tool = await _context.LanguageTools.FindAsync(request.Id, cancellationToken);
            if (tool == null)
            {
                throw new NotFoundException(nameof(LanguageTool), request.Id);
            }
            tool.Title = request.Title;
            tool.IsActive = request.IsActive;
            tool.ImagePath = request.ImagePath;
            tool.Description = request.Description;
            tool.TechnologyId = request.TechnologyId;
            tool.GroupId = request.GroupId;

            await _context.SaveChangesAsync(cancellationToken);
            return tool.Id;
        }
    }
}
