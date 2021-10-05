using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Application.Categories.Commands.CreateCategory
{
  public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
  {
    public CreateCategoryCommandValidator()
    {
      RuleFor(command => command.Name)
        .NotNull().WithMessage("Name cannot be null")
        .NotEmpty().WithMessage("Name cannot be empty")
        .MinimumLength(2).WithMessage("Name must be longer than 2")
        .MaximumLength(20).WithMessage("Name must be shorter than 20");
    }
  }
}
