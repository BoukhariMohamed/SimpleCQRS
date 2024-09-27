using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCQRS.Application.Commands.Validators
{
    public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
    {
        public CreatePostCommandValidator()
        {
          RuleFor(x => x.title).NotNull().NotEmpty().WithMessage("Title is required.");
          RuleFor(x => x.content).NotNull().NotEmpty().WithMessage("Content is required.");
        }
    }
}
