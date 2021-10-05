using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Application.Developers.Commands.CreateDeveloper
{
  public class CreateDeveloperCommandValidator : AbstractValidator<CreateDeveloperCommand>
  {
    public CreateDeveloperCommandValidator()
    {
      RuleFor(command => command.Name)
        .NotEmpty().WithMessage("Name should not be empty")
        .MinimumLength(2).WithMessage("Name must be longer than 2")
        .MaximumLength(20).WithMessage("Name must be shorter than 20");

      RuleFor(command => command.Country)
        .NotEmpty().WithMessage("Country should not be empty")
        .MinimumLength(2).WithMessage("Country must be longer than 2")
        .MaximumLength(20).WithMessage("Country must be shorter than 20");
    }
  }
}
