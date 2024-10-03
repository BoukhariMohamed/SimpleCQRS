using FluentValidation;

namespace SimpleCQRS.Application.Commands.Validators;

public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
{
    public CreatePostCommandValidator()
    {
      RuleFor(x => x.title).NotNull().NotEmpty().WithMessage("Title is required.");
      RuleFor(x => x.content).NotNull().NotEmpty().WithMessage("Content is required.");
    }
}
