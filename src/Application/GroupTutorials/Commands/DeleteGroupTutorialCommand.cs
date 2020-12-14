using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Application.Common.Exceptions;
using Application.Common.Interfaces;

namespace Application.GroupTutorials.Commands
{
    public class DeleteGroupTutorialCommand : IRequest
    {
        public int Id { get; set; }
    }

    internal class DeleteGroupTutorialCommandHandler : IRequestHandler<DeleteGroupTutorialCommand>
    {
        private readonly IAppDbContext _context;

        public DeleteGroupTutorialCommandHandler(IAppDbContext context)
        {
            _context = context;
        }
        public async Task<Unit> Handle(DeleteGroupTutorialCommand request, CancellationToken cancellationToken)
        {
            var exists = await _context.GroupTutorials.FindAsync(request.Id);
            if(exists == null)
            {
                throw new NotFoundException(nameof(GroupLanguageTool), request.Id);
            }
            _context.GroupTutorials.Remove(exists);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
