using FluentValidation;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Application.Common.Interfaces;

namespace Application.GroupTutorials.Commands.Validators
{
    public class CreateGroupTutorialCommandValidator : AbstractValidator<CreateGroupTutorialCommand>
    {
        private readonly IAppDbContext _context;

        public CreateGroupTutorialCommandValidator(IAppDbContext context)
        {
            _context = context;

            RuleFor(v => v.Title)
                .NotEmpty().WithMessage("Title is required");
        }

        public async Task<bool> BeUniqueTitle(string title, CancellationToken cancellationToken)
        {
            return await _context.Articles.AllAsync(t => t.Title != title, cancellationToken);
        }
    }
}
