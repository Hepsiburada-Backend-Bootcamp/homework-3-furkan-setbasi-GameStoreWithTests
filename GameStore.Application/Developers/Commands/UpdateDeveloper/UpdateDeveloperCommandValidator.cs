using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Application.Developers.Commands.UpdateDeveloper
{
  public class UpdateDeveloperCommandValidator : AbstractValidator<UpdateDeveloperCommand>
  {
    public UpdateDeveloperCommandValidator()
    {
      RuleFor(command => command.Name)
        .MinimumLength(2).WithMessage("Name must be longer than 2")
        .MaximumLength(20).WithMessage("Name must be shorter than 20")
        .When(command => command.Name != string.Empty);

      RuleFor(command => command.Country)
        .MinimumLength(2).WithMessage("Country must be longer than 2")
        .MaximumLength(20).WithMessage("Country must be shorter than 20") 
        .When(command => command.Country != string.Empty);
    }
  }
}
