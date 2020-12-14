using FluentValidation;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Application.GroupLanguageTools.Commands.Validators
{
    public class UpdateGLTCommandValidator : AbstractValidator<UpdateGLTCommand>
    {
        private readonly IAppDbContext _context;

        public UpdateGLTCommandValidator(IAppDbContext context)
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
