using GameStore.Application.Games.Commands.UpdateGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GameStore.UnitTests.Application.Games.Commands.UpdateGame
{
  public class UpdateGameCommandValidatorTests
  {
    [Fact]
    public async Task Validator_WhenValidInputsAreGiven_ShouldNotReturnError()
    {
      UpdateGameCommand command = new()
      {
        Name = "game",
        Price = 60
      };

      UpdateGameCommandValidator validator = new();
      var validationResult = await validator.ValidateAsync(command);

      Assert.Empty(validationResult.Errors);
    }

    [Theory]
    [InlineData("a", -1)]
    [InlineData("valid", -1)]
    [InlineData("a", 0)]
    [InlineData("a", 60)]
    [InlineData("an invalid game name that is longer than 20 characters", 60)]
    public async Task Validator_WhenInvalidInputsAreGiven_ShouldNotReturnError(string name, int price)
    {
      UpdateGameCommand command = new()
      {
        Name = name,
        Price = price
      };

      UpdateGameCommandValidator validator = new();
      var validationResult = await validator.ValidateAsync(command);

      Assert.NotEmpty(validationResult.Errors);
    }
  }
}
