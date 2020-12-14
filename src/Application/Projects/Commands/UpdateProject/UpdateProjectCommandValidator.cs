using FluentValidation;
using Domain.Entities;
using Application.Common.Interfaces;

namespace Application.Projects.Commands.UpdateProject
{
    public class UpdateProjectCommandValidator : AbstractValidator<Project>
    {
        private readonly IAppDbContext _context;

        public UpdateProjectCommandValidator(IAppDbContext context)
        {
            _context = context;

            RuleFor(v => v.Title)
                .NotEmpty().WithMessage("Title is required");
        }
    }
}
