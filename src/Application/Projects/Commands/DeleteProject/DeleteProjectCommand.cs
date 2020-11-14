using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Application.Common.Exceptions;
using Application.Common.Interfaces;

namespace Application.Projects.Commands.DeleteProject
{
    public class DeleteProjectCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteProjectCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Projects.FindAsync(request.Id);
            if(entity == null)
            {
                throw new NotFoundException(nameof(Project), request.Id);
            }

            _context.Projects.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
