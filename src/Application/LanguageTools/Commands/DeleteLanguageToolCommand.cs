using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Application.Common.Exceptions;
using Application.Common.Interfaces;

namespace Application.LanguageTools.Commands
{
    public class DeleteLanguageToolCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteLanguageToolCommandHandler : IRequestHandler<DeleteLanguageToolCommand>
    {
        private readonly IAppDbContext _context;

        public DeleteLanguageToolCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteLanguageToolCommand request, CancellationToken cancellationToken)
        {
            var exists = await _context.LanguageTools.FindAsync(request.Id);
            if (exists == null)
            {
                throw new NotFoundException(nameof(LanguageTool), request.Id);
            }
            _context.LanguageTools.Remove(exists);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
