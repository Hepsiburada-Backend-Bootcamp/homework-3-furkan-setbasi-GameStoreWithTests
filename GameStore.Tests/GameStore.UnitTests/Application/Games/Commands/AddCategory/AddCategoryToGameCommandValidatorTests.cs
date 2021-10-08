using GameStore.Application.Games.Commands.AddCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GameStore.UnitTests.Application.Games.Commands.AddCategory
{
  public class AddCategoryToGameCommandValidatorTests
  {
    [Fact]
    public async Task Validator_WhenValidInputsAreGiven_ShouldNotReturnError()
    {
      AddCategoryToGameCommand command = new()
      {
        Name = "game",
      };

      AddCategoryToGameCommandValidator validator = new();
      var validationResult = await validator.ValidateAsync(command);

      Assert.Empty(validationResult.Errors);
    }

    [Theory]
    [InlineData("a")]
    [InlineData(" ")]
    [InlineData("  ")]
    [InlineData("   ")]
    [InlineData("an invalid category name that is longer than 20 characters")]
    public async Task Validator_WhenInvalidInputsAreGiven_ShouldNotReturnError(string name)
    {
      AddCategoryToGameCommand command = new()
      {
        Name = name,
      };

      AddCategoryToGameCommandValidator validator = new();
      var validationResult = await validator.ValidateAsync(command);

      Assert.NotEmpty(validationResult.Errors);
    }
  }
}
