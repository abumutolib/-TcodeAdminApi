using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Application.Common.Exceptions;
using Application.Common.Interfaces;

namespace Application.Technologies.Commands
{
    public class UpdateTechnologyCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public bool IsActive { get; set; }
    }

    internal class UpdateTechnologyCommandHandler : IRequestHandler<UpdateTechnologyCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public UpdateTechnologyCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(UpdateTechnologyCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Technologies.FindAsync(request.Id);
            if (entity == null)
            {
                throw new NotFoundException(nameof(Technology), request.Id);
            }
            entity.Title = request.Title;
            entity.Text = request.Text;
            entity.IsActive = request.IsActive;
            await _context.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }
    }
}
