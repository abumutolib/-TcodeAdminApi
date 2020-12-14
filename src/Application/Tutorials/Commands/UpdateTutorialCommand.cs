using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Application.Common.Exceptions;
using Application.Common.Interfaces;

namespace Application.Tutorials.Commands
{
    public class UpdateTutorialCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public bool IsActive { get; set; }
    }

    public class UpdateTutorialCommandHandler : IRequestHandler<UpdateTutorialCommand, int>
    {
        private readonly IAppDbContext _context;

        public UpdateTutorialCommandHandler(IAppDbContext context)
        {
            _context = context;
        }
        public async Task<int> Handle(UpdateTutorialCommand request, CancellationToken cancellationToken)
        {
            var tutorial = await _context.Tutorials.FindAsync(request.Id);
            if (tutorial == null)
            {
                throw new NotFoundException(nameof(Tutorial), request.Id);
            }
            tutorial.Title = request.Title;
            tutorial.HtmlContent = request.Content;
            tutorial.IsActive = request.IsActive;
            await _context.SaveChangesAsync(cancellationToken);
            return tutorial.Id;
        }
    }
}
