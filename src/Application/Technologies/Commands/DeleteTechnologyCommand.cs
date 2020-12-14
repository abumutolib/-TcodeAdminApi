using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Application.Common.Exceptions;
using Application.Common.Interfaces;

namespace Application.Technologies.Commands
{
    public class DeleteTechnologyCommand : IRequest
    {
        public int Id { get; set; }
    }

    internal class DeleteTechnologyCommandHandler : IRequestHandler<DeleteTechnologyCommand>
    {
        private readonly IAppDbContext _context;

        public DeleteTechnologyCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteTechnologyCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Technologies.FindAsync(request.Id);
            if (entity == null)
            {
                throw new NotFoundException(nameof(Technology), request.Id);
            }
            _context.Technologies.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
