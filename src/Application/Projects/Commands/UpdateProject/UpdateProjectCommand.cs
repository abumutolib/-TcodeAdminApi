using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Application.Common.Exceptions;
using Application.Common.Interfaces;

namespace Application.Projects.Commands.UpdateProject
{
    public class UpdateProjectCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }

    public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public UpdateProjectCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _context.Projects.FindAsync(request.Id);
            if(project == null)
            {
                throw new NotFoundException(nameof(Project), request.Id);
            }

            project.Title = request.Title;

            await _context.SaveChangesAsync(cancellationToken);
            return project.Id;
        }
    }
}
