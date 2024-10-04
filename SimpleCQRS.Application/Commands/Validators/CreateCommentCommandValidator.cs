using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCQRS.Application.Commands.Validators
{
    public class CreateCommentCommandValidator : AbstractValidator<CreateCommantCommand>
    {
        public CreateCommentCommandValidator() 
        {
            RuleFor(x=>x.PostId).NotNull().NotEmpty().WithMessage("Id Post is required.");
            RuleFor(x=>x.Text).NotEmpty().NotNull().WithMessage("Title is required.");
        }
    }
}
