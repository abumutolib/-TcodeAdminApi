using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Application.Common.Exceptions;
using Application.Common.Interfaces;

namespace Application.GroupLanguageTools.Commands
{
    public class DeleteGLTCommand : IRequest
    {
        public int Id { get; set; }
    }

    internal class DeleteGLTCommandHandler : IRequestHandler<DeleteGLTCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteGLTCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteGLTCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.GroupLanguageTools.FindAsync(request.Id);
            if (entity == null)
                throw new NotFoundException(nameof(GroupLanguageTool), request.Id);

            _context.GroupLanguageTools.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
