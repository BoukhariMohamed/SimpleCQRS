using FluentValidation;

namespace SimpleCQRS.Application.Commands.Validators
{
    public class UpdatePostCommandValidator : AbstractValidator<UpdatePostCommand>
    {
        public UpdatePostCommandValidator()
        {
            RuleFor(x => x.Title).NotNull().NotEmpty().WithMessage("Title is required.");
            RuleFor(x => x.Content).NotNull().NotEmpty().WithMessage("Content is required.");
        }
    }
}
