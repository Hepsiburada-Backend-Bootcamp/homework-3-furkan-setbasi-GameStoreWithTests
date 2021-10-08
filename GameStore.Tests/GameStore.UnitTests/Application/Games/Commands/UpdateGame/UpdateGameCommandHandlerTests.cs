using FluentAssertions;
using GameStore.Application.Common.Exceptions;
using GameStore.Application.Games.Commands.UpdateGame;
using GameStore.Domain.Entities;
using GameStore.UnitTests.TestSetup;
using GameStore.UnitTests.TestSetup.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace GameStore.UnitTests.Application.Games.Commands.UpdateGame
{
  public class UpdateGameCommandHandlerTests : IClassFixture<CommonTestFixture>
  {
    [Fact]
    public async void Handle_WhenCommandWithExistingGameIdIsGiven_ShouldUpdateGameWithGivenId()
    {
      // Arrange
      var gameRepository = MockGameRepository.GetMockGameRepository().Object;

      var game = new Game()
      {
        Name = Guid.NewGuid().ToString(),
        Price = 1,
      };

      Guid createdGameId = await gameRepository.CreateAsync(game, CancellationToken.None);

      var request = new UpdateGameCommand()
      {
        Id = createdGameId,
        Name = Guid.NewGuid().ToString(),
        Price = 2
      };

      var handler = new UpdateGameCommandHandler(gameRepository);

      // Act
      await handler.Handle(request, CancellationToken.None);

      // Assert
      var updatedGame = await gameRepository.GetByIdAsync(createdGameId, CancellationToken.None);

      updatedGame.Name.Should().Be(request.Name);
      updatedGame.Price.Should().Be(request.Price);

    }

    [Fact]
    public void Handle_WhenCommandWithNonExistingGameIdIsGiven_ThrowsNotFoundException()
    {
      // Arrange
      var gameRepository = MockGameRepository.GetMockGameRepository().Object;

      var request = new UpdateGameCommand()
      {
        Id = Guid.NewGuid(),
        Name = Guid.NewGuid().ToString()
      };

      var handler = new UpdateGameCommandHandler(gameRepository);

      // Act & Assert
      FluentActions
        .Invoking(async () => await handler.Handle(request, CancellationToken.None))
        .Should()
        .ThrowAsync<NotFoundException>()
        .Result.And.Message.Should().Be("Game was not found");
    }

    [Fact]
    public async void Handle_WhenCommandWithEmptyGameNameIsGiven_ShouldNotUpdateGameName()
    {
      // Arrange
      var gameRepository = MockGameRepository.GetMockGameRepository().Object;

      var game = new Game()
      {
        Name = Guid.NewGuid().ToString(),
        Price = 99
      };

      Guid createdGameId = await gameRepository.CreateAsync(game, CancellationToken.None);

      var request = new UpdateGameCommand()
      {
        Id = createdGameId,
        Name = "",
        Price = 60
      };

      var handler = new UpdateGameCommandHandler(gameRepository);

      // Act
      await handler.Handle(request, CancellationToken.None);

      // Assert
      var updatedGame = await gameRepository.GetByIdAsync(createdGameId, CancellationToken.None);

      updatedGame.Name.Should().Be(game.Name);
      updatedGame.Price.Should().Be(request.Price);
    }

    [Fact]
    public async void Handle_WhenCommandWithZeroGamePriceIsGiven_ShouldNotUpdateGamePrice()
    {
      // Arrange
      var gameRepository = MockGameRepository.GetMockGameRepository().Object;

      var game = new Game()
      {
        Name = Guid.NewGuid().ToString(),
        Price = 99
      };

      Guid createdGameId = await gameRepository.CreateAsync(game, CancellationToken.None);

      var request = new UpdateGameCommand()
      {
        Id = createdGameId,
        Name = "game",
        Price = 0
      };

      var handler = new UpdateGameCommandHandler(gameRepository);

      // Act
      await handler.Handle(request, CancellationToken.None);

      // Assert
      var updatedGame = await gameRepository.GetByIdAsync(createdGameId, CancellationToken.None);

      updatedGame.Name.Should().Be(request.Name);
      updatedGame.Price.Should().Be(game.Price);
    }
  }
}
