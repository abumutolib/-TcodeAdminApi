using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Application.Common.Exceptions;
using Application.Common.Interfaces;

namespace Application.Articles.Commands
{
    public class UpdateArticleCommand : IRequest
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public bool IsActive { get; set; }
    }

    public class UpdateArticleCommandHandler : IRequestHandler<UpdateArticleCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateArticleCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateArticleCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Articles.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Article), request.Id);
            }

            entity.Title = request.Title;
            entity.HtmlContent = request.Content;
            entity.IsActive = request.IsActive;

            return Unit.Value;
        }
    }
}
