using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Application.Common.Interfaces;

namespace Application.GroupTutorials.Commands
{
    public class CreateGroupTutorialCommand : IRequest<int>
    {
        public string Title { get; set; }
        public bool IsActive { get; set; }
    }

    internal class CreateGroupTutorialCommandHandler : IRequestHandler<CreateGroupTutorialCommand, int>
    {
        private readonly IAppDbContext _context;

        public CreateGroupTutorialCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateGroupTutorialCommand request, CancellationToken cancellationToken)
        {
            var groupTutorial = new GroupTutorial
            {
                Title = request.Title,
                IsActive = request.IsActive
            };
            _context.GroupTutorials.Add(groupTutorial);
            await _context.SaveChangesAsync(cancellationToken);
            return groupTutorial.Id;
        }
    }
}