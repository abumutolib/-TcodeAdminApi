using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Application.Common.Exceptions;
using Application.Common.Interfaces;

namespace Application.GroupLanguageTools.Commands
{
    public class UpdateGLTCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsActive { get; set; }
    }

    internal class UpdateGLTCommandHandler : IRequestHandler<UpdateGLTCommand, int>
    {
        private readonly IAppDbContext _context;

        public UpdateGLTCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(UpdateGLTCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.GroupLanguageTools.FindAsync(request.Id);
            if (entity == null)
                throw new NotFoundException(nameof(GroupLanguageTool), request.Id);

            entity.Title = request.Title;
            entity.IsActive = request.IsActive;
            _context.GroupLanguageTools.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
