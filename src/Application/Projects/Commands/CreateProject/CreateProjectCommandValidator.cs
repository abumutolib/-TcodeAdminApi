using FluentValidation;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Application.Common.Interfaces;

namespace Application.Projects.Commands.CreateProject
{
    public class CreateProjectCommandValidator : AbstractValidator<Project>
    {
        private readonly IAppDbContext _context;

        public CreateProjectCommandValidator(IAppDbContext context)
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
