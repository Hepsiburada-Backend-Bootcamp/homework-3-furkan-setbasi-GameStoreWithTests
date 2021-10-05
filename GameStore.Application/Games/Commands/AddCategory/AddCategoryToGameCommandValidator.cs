using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Application.Games.Commands.AddCategory
{
  public class AddCategoryToGameCommandValidator : AbstractValidator<AddCategoryToGameCommand>
  {
    public AddCategoryToGameCommandValidator()
    {
      RuleFor(command => command.Name)
        .NotEmpty().WithMessage("Name should not be empty")
        .MinimumLength(2).WithMessage("Name must be longer than 2")
        .MaximumLength(20).WithMessage("Name must be shorter than 20");
    }
  }
}
