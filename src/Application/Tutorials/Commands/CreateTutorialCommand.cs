using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Application.Common.Interfaces;

namespace Application.Tutorials.Commands
{
    public class CreateTutorialCommand : IRequest<int>
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public int GroupId { get; set; }
        public int ToolId { get; set; }
    }

    public class CreateTutorialCommandHandler : IRequestHandler<CreateTutorialCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateTutorialCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<int> Handle(CreateTutorialCommand request, CancellationToken cancellationToken)
        {
            var tutorial = new Tutorial
            {
                Title = request.Title,
                HtmlContent = request.Content,
                GroupId = request.GroupId,
                LanguageToolId = request.ToolId
            };
            _context.Tutorials.Add(tutorial);
            await _context.SaveChangesAsync(cancellationToken);
            return tutorial.Id;
        }
    }
}
