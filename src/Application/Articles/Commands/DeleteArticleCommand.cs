using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Application.Common.Exceptions;
using Application.Common.Interfaces;

namespace Application.Articles.Commands
{
    public class DeleteArticleCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteArticleCommandHandler : IRequestHandler<DeleteArticleCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteArticleCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteArticleCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Articles.FindAsync(request.Id);
            if (entity == null)
            {
                throw new NotFoundException(nameof(Article), request.Id);
            }
            _context.Articles.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
