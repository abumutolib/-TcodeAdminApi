using FluentValidation;

namespace Application.Articles.Commands.Validators
{
    public class UpdateArticleCommandValidator : AbstractValidator<UpdateArticleCommand>
    {
        public UpdateArticleCommandValidator()
        {
            RuleFor(v => v.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(2048).WithMessage("Title must not exceed 2048 characters.")
                .MinimumLength(10).WithMessage("Title must not exceed min 10 characters.");
            RuleFor(v => v.Content)
                .NotEmpty().WithMessage("Content is required.")
                .MinimumLength(10).WithMessage("");
        }
    }
}
