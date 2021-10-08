using GameStore.Application.Developers.Commands.UpdateDeveloper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GameStore.UnitTests.Application.Developers.Commands.UpdateDeveloper
{
  public class UpdateDeveloperCommandValidatorTests
  {
    [Fact]
    public async Task Validator_WhenValidInputsAreGiven_ShouldNotReturnError()
    {
      UpdateDeveloperCommand command = new()
      {

        Name = "category",
        Country = "country"
      };

      UpdateDeveloperCommandValidator validator = new();
      var validationResult = await validator.ValidateAsync(command);

      Assert.Empty(validationResult.Errors);
    }

    [Theory]
    [InlineData("a", "a")]
    [InlineData("valid", "invalid country name that is longer than 20 characters")]
    [InlineData("invalid developer name that is longer than 20 characters", "valid")]
    public async Task Validator_WhenInvalidInputsAreGiven_ShouldNotReturnError(string name, string country)
    {
      UpdateDeveloperCommand command = new()
      {
        Name = name,
        Country = country
      };

      UpdateDeveloperCommandValidator validator = new();
      var validationResult = await validator.ValidateAsync(command);

      Assert.NotEmpty(validationResult.Errors);
    }
  }
}
