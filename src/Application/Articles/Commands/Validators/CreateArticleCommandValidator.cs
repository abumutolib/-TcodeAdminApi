using FluentValidation;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Application.Common.Interfaces;

namespace Application.Articles.Commands.Validators
{
    public class CreateArticleCommandValidator : AbstractValidator<CreateArticleCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateArticleCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(2048).WithMessage("Title must not exceed 2048 characters.")
                .MinimumLength(10).WithMessage("Title must not exceed min 10 characters.")
                .MustAsync(BeUniqueTitle).WithMessage("The specified title already exists.");
            RuleFor(v => v.Content)
                .NotEmpty().WithMessage("Content is required.")
                .MinimumLength(10).WithMessage("");

        }

        public async Task<bool> BeUniqueTitle(string title, CancellationToken cancellationToken)
        {
            return await _context.Articles.AllAsync(t => t.Title != title, cancellationToken);
        }
    }
}
