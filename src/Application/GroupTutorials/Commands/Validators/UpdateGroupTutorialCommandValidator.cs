using FluentValidation;
using Domain.Entities;

namespace Application.GroupTutorials.Commands.Validators
{
    public class UpdateGroupTutorialCommandValidator : AbstractValidator<GroupLanguageTool>
    {
        public UpdateGroupTutorialCommandValidator()
        {
            RuleFor(v => v.Title)
                .NotEmpty().WithMessage("Title is required");
        }
    }
}
