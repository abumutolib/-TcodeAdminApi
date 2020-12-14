using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Application.Common.Interfaces;

namespace Application.LanguageTools.Commands
{
    public class CreateLanguageToolCommand : IRequest<int>
    {
        public string Title { get; set; }
        public bool IsActive { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }
        public int TechnologyId { get; set; }
        public int GroupId { get; set; }
    }

    public class CreateLanguageToolCommandHandler : IRequestHandler<CreateLanguageToolCommand, int>
    {
        private readonly IAppDbContext _context;

        public CreateLanguageToolCommandHandler(IAppDbContext context)
        {
            _context = context;
        }
        public async Task<int> Handle(CreateLanguageToolCommand request, CancellationToken cancellationToken)
        {
            var tool = new LanguageTool
            {
                Title = request.Title,
                IsActive = request.IsActive,
                ImagePath = request.ImagePath,
                Description = request.Description,
                TechnologyId = request.TechnologyId,
                GroupId = request.GroupId
            };
            _context.LanguageTools.Add(tool);
            await _context.SaveChangesAsync(cancellationToken);
            return tool.Id;
        }
    }
}
