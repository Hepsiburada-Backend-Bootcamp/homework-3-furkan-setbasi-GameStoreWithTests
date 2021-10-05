using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Application.Categories.Commands.UpdateCategory
{
  public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
  {
    public UpdateCategoryCommandValidator()
    {
      RuleFor(command => command.Name)
        .MinimumLength(2).WithMessage("Name must be longer than 2")
        .MaximumLength(20).WithMessage("Name must be shorter than 20")
        .When(command => !string.IsNullOrWhiteSpace(command.Name));
    }
  }
}
