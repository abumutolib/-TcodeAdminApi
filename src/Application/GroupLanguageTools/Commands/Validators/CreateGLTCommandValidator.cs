using FluentValidation;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Application.Common.Interfaces;

namespace Application.GroupLanguageTools.Commands.Validators
{
    public class CreateGLTCommandValidator : AbstractValidator<CreateGLTCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateGLTCommandValidator(IApplicationDbContext context)
        {
            _context = context;
            RuleFor(v => v.Title)
                .NotEmpty().WithMessage("Title is required")
                .MustAsync(BeUniqueTitle).WithMessage("The specified title already exists");
        }

        public async Task<bool> BeUniqueTitle(string title, CancellationToken cancellationToken)
        {
            return await _context.Articles.AllAsync(t => t.Title != title, cancellationToken);
        }
    }
}
