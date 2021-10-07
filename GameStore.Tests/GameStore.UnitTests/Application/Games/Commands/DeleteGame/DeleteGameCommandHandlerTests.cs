using FluentAssertions;
using GameStore.Application.Common.Exceptions;
using GameStore.Application.Games.Commands.DeleteGame;
using GameStore.Domain.Entities;
using GameStore.UnitTests.TestSetup.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace GameStore.UnitTests.Application.Games.Commands.DeleteGame
{
  public class DeleteGameCommandHandlerTests
  {
    [Fact]
    public async void Handle_WhenCommandWithExistingGameIdIsGiven_ShouldDeleteGameWithGivenId()
    {
      // Arrange
      var gameRepository = MockGameRepository.GetMockGameRepository().Object;

      var game = new Game()
      {
        Name = Guid.NewGuid().ToString(),
        Price = 99
      };

      Guid createdGameId = await gameRepository.CreateAsync(game, CancellationToken.None);

      var request = new DeleteGameCommand() { Id = createdGameId };

      var handler = new DeleteGameCommandHandler(gameRepository);

      // Act
      await handler.Handle(request, CancellationToken.None);

      // Assert
      var deletedGame = await gameRepository.GetByIdAsync(createdGameId, CancellationToken.None);

      deletedGame.Should().BeNull();
    }

    [Fact]
    public void Handle_WhenCommandWithNonExistingGameIdIsGiven_ThrowsNotFoundException()
    {
      // Arrange
      var gameRepository = MockGameRepository.GetMockGameRepository().Object;

      var request = new DeleteGameCommand()
      {
        Id = Guid.NewGuid()
      };

      var handler = new DeleteGameCommandHandler(gameRepository);

      // Act & Assert
      FluentActions
        .Invoking(async () => await handler.Handle(request, CancellationToken.None))
        .Should().ThrowAsync<NotFoundException>().Result
        .And
        .Message.Should().Be("Game was not found");
    }
  }
}
