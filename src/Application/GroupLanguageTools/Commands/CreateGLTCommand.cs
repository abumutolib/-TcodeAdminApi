using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Application.Common.Interfaces;

namespace Application.GroupLanguageTools.Commands
{
    public class CreateGLTCommand : IRequest<int>
    {
        public string Title { get; set; }
        public bool IsActive { get; set; }
    }

    internal class CreateGLTCommandHandler : IRequestHandler<CreateGLTCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateGLTCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateGLTCommand request, CancellationToken cancellationToken)
        {
            var entity = new GroupLanguageTool
            {
                Title = request.Title,
                IsActive = request.IsActive
            };

            _context.GroupLanguageTools.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
