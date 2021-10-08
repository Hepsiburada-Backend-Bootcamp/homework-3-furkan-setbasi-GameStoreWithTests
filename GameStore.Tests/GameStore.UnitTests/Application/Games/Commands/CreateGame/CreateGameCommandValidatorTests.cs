using GameStore.Application.Games.Commands.CreateGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GameStore.UnitTests.Application.Games.Commands.CreateGame
{
  public class CreateGameCommandValidatorTests
  {
    [Fact]
    public async Task Validator_WhenValidInputsAreGiven_ShouldNotReturnError()
    {
      CreateGameCommand command = new()
      {
        Name = "game",
        Price = 60
      };

      CreateGameCommandValidator validator = new();
      var validationResult = await validator.ValidateAsync(command);

      Assert.Empty(validationResult.Errors);
    }

    [Theory]
    [InlineData("a", 0)]
    [InlineData("valid", 0)]
    [InlineData("", 60)]
    [InlineData(" ", 60)]
    [InlineData("   ", 60)]
    [InlineData("an invalid game name that is longer than 20 characters", 60)]
    public async Task Validator_WhenInvalidInputsAreGiven_ShouldNotReturnError(string name, int price)
    {
      CreateGameCommand command = new()
      {
        Name = name,
        Price = price
      };

      CreateGameCommandValidator validator = new();
      var validationResult = await validator.ValidateAsync(command);

      Assert.NotEmpty(validationResult.Errors);
    }
  }
}
