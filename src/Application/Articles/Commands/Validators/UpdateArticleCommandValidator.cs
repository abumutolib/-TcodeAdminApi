using FluentValidation;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Application.Common.Interfaces;
using System.Linq;

namespace Application.Articles.Commands.Validators
{
    public class UpdateArticleCommandValidator : AbstractValidator<UpdateArticleCommand>
    {
        private readonly IAppDbContext _context;

        public UpdateArticleCommandValidator(IAppDbContext context)
        {
            _context = context;
        }

        public UpdateArticleCommandValidator()
        {
            RuleFor(v => v.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(2048).WithMessage("Title must not exceed 2048 characters.")
                .MinimumLength(10).WithMessage("Title must not exceed min 10 characters.")
                .MustAsync((x, t, c) => BeUniqueTitle(x.Id, t, c));
            RuleFor(v => v.Content)
                .NotEmpty().WithMessage("Content is required.")
                .MinimumLength(10).WithMessage("");
        }

        public async Task<bool> BeUniqueTitle(int id, string title, CancellationToken cancellationToken)
        {
            return await _context.Articles.Where(x => x.Id != id).AllAsync(t => t.Title != title, cancellationToken);
        }
    }
}
