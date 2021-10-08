using GameStore.Application.Categories.Commands.UpdateCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GameStore.UnitTests.Application.Categories.Commands.UpdateCategory
{
  public class UpdateCategoryCommandValidatorTests
  {
    [Fact]
    public async Task Validator_WhenValidInputsAreGiven_ShouldNotReturnErros()
    {
      UpdateCategoryCommand command = new()
      {
        Id = Guid.NewGuid(),
        Name = "Valid Category Name"
      };

      UpdateCategoryCommandValidator validator = new();
      var validationResult = await validator.ValidateAsync(command);

      Assert.Empty(validationResult.Errors);
    }

    [Theory]
    [InlineData("a")]
    [InlineData("An invalid category name that is longer than 20 characters")]
    public async Task Validator_WhenInvalidInputsAreGiven_ShouldReturnErrors(string categoryName)
    {
      UpdateCategoryCommand command = new()
      {
        Id = Guid.NewGuid(),
        Name = categoryName
      };

      UpdateCategoryCommandValidator validator = new();
      var validationResult = await validator.ValidateAsync(command);

      Assert.NotEmpty(validationResult.Errors);
    }

  }
}
