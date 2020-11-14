using FluentValidation;
using Domain.Entities;
using Application.Common.Interfaces;

namespace Application.Projects.Commands.UpdateProject
{
    public class UpdateProjectCommandValidator : AbstractValidator<Project>
    {
        private readonly IApplicationDbContext _context;

        public UpdateProjectCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.Title)
                .NotEmpty().WithMessage("Title is required");
        }
    }
}
