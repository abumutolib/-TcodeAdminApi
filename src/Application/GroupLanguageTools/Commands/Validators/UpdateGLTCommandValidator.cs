using FluentValidation;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Application.GroupLanguageTools.Commands.Validators
{
    public class UpdateGLTCommandValidator : AbstractValidator<UpdateGLTCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateGLTCommandValidator(IApplicationDbContext context)
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
