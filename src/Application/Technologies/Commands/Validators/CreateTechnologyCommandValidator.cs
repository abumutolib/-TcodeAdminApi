using FluentValidation;

namespace Application.Technologies.Commands.Validators
{
    public class CreateTechnologyCommandValidator : AbstractValidator<CreateTechnologyCommand>
    {
        public CreateTechnologyCommandValidator()
        {
            RuleFor(v => v.Title)
                .NotEmpty().WithMessage("Title is required");
            RuleFor(v => v.Text)
                .NotEmpty().WithMessage("Text is required");
        }
    }
}
