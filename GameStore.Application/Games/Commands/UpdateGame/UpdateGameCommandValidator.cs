using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Application.Games.Commands.UpdateGame
{
  public class UpdateGameCommandValidator : AbstractValidator<UpdateGameCommand>
  {
    public UpdateGameCommandValidator()
    {
      RuleFor(command => command.Name)
        .MinimumLength(2).WithMessage("Name must be longer than 2")
        .MaximumLength(20).WithMessage("Name must be shorter than 20")
        .When(command => command.Name != string.Empty);

      RuleFor(command => command.Price)
        .GreaterThan(-1)
        .WithMessage("Price can not be a negative number. If you don't want to update this field, please provide 0.");
    }
  }
}
