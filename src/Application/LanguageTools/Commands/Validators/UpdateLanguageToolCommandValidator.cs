using FluentValidation;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Application.Common.Interfaces;

namespace Application.LanguageTools.Commands.Validators
{
    public class UpdateLanguageToolCommandValidator : AbstractValidator<UpdateLanguageToolCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateLanguageToolCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.Title)
                .NotEmpty().WithMessage("Title is required")
                .MustAsync(BeUniqueTitle).WithMessage("The specified title already exists");
            RuleFor(v => v.Description)
                .NotEmpty().WithMessage("Description is required");
        }

        public async Task<bool> BeUniqueTitle(string title, CancellationToken cancellationToken)
        {
            return await _context.LanguageTools.AnyAsync(t => t.Title != title, cancellationToken);
        }
    }
}
