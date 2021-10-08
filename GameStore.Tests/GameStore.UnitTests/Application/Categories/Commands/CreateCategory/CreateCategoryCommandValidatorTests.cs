using GameStore.Application.Categories.Commands.CreateCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GameStore.UnitTests.Application.Categories.Commands.CreateCategory
{
  public class CreateCategoryCommandValidatorTests
  {
    [Fact]
    public async Task Validator_WhenValidInputsAreGiven_ShouldNotReturnError()
    {
      CreateCategoryCommand command = new()
      {
        Name = "Valid Category Name",
      };

      CreateCategoryCommandValidator validator = new();
      var validationResult = await validator.ValidateAsync(command);

      Assert.Empty(validationResult.Errors);
    }

    [Theory]
    [InlineData(" ")]
    [InlineData("  ")]
    [InlineData("An invalid category name that is longer than 20 characters")]
    public async Task Validator_WhenInvalidInputsAreGiven_ShouldReturnErrors(string categoryName)
    {
      CreateCategoryCommand command = new()
      {
        Name = categoryName
      };

      CreateCategoryCommandValidator validator = new();
      var validationResult = await validator.ValidateAsync(command);

      Assert.NotEmpty(validationResult.Errors);
    }
  }
}
