using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Application.Common.Exceptions;
using Application.Common.Interfaces;

namespace Application.Tutorials.Commands
{
    public class DeleteTutorialCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteTutorialCommandHandler : IRequestHandler<DeleteTutorialCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteTutorialCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Unit> Handle(DeleteTutorialCommand request, CancellationToken cancellationToken)
        {
            var tutorial = await _context.Tutorials.FindAsync(request.Id, cancellationToken);
            if (tutorial == null)
            {
                throw new NotFoundException(nameof(Tutorial), request.Id);
            }
            _context.Tutorials.Remove(tutorial);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
