using FluentValidation;

namespace Application.Tutorials.Commands.Validators
{
    public class CreateTutorialCommandValidator : AbstractValidator<CreateTutorialCommand>
    {
        public CreateTutorialCommandValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required");
            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Html content is required");
        }
    }
}
