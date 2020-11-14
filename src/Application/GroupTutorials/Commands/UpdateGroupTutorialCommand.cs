using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Application.Common.Exceptions;
using Application.Common.Interfaces;

namespace Application.GroupTutorials.Commands
{
    public class UpdateGroupTutorialCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsActive { get; set; }
    }

    internal class UpdateGroupTutorialCommandHandler : IRequestHandler<UpdateGroupTutorialCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public UpdateGroupTutorialCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(UpdateGroupTutorialCommand request, CancellationToken cancellationToken)
        {
            var exists = await _context.GroupTutorials.FindAsync(request.Id);
            if(exists == null)
            {
                throw new NotFoundException(nameof(GroupLanguageTool), request.Id);
            }
            exists.Title = request.Title;
            exists.IsActive = request.IsActive;
            await _context.SaveChangesAsync(cancellationToken);
            return exists.Id;
        }
    }
}
