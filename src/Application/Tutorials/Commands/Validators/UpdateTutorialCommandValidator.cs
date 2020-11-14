using FluentValidation;

namespace Application.Tutorials.Commands.Validators
{
    public class UpdateTutorialCommandValidator : AbstractValidator<UpdateTutorialCommand>
    {
        public UpdateTutorialCommandValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required");
        }
    }
}
