using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Application.Common.Interfaces;

namespace Application.Technologies.Commands
{
    public class CreateTechnologyCommand : IRequest<int>
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public bool IsActive { get; set; }
    }

    internal class CreateTechnologyCommandHandler : IRequestHandler<CreateTechnologyCommand, int>
    {
        private readonly IAppDbContext _context;

        public CreateTechnologyCommandHandler(IAppDbContext context)
        {
            _context = context;
        }
        public async Task<int> Handle(CreateTechnologyCommand request, CancellationToken cancellationToken)
        {
            var tech = new Technology
            {
                Title = request.Title,
                Text = request.Text,
                IsActive = request.IsActive
            };

            _context.Technologies.Add(tech);
            await _context.SaveChangesAsync(cancellationToken);
            return tech.Id;
        }
    }
}
