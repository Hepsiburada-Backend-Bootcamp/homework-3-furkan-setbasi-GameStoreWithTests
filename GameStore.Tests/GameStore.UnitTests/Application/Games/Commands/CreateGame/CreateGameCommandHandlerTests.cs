using AutoMapper;
using FluentAssertions;
using GameStore.Application.Games.Commands.CreateGame;
using GameStore.UnitTests.TestSetup;
using GameStore.UnitTests.TestSetup.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace GameStore.UnitTests.Application.Games.Commands.CreateGame
{
  public class CreateGameCommandHandlerTests : IClassFixture<CommonTestFixture>
  {
    private readonly IMapper _mapper;

    public CreateGameCommandHandlerTests(CommonTestFixture testFixture)
    {
      _mapper = testFixture.Mapper;
    }

    [Fact]
    public async void Handle_ShouldReturnCreatedGameId()
    {
      // Arrange
      var gameRepository = MockGameRepository.GetMockGameRepository().Object;

      var request = new CreateGameCommand()
      {
        Name = Guid.NewGuid().ToString(),
        Price = 99
      };

      var handler = new CreateGameCommandHandler(gameRepository, _mapper);

      // Act
      Guid result = await handler.Handle(request, CancellationToken.None);
      var gameList = await gameRepository.GetAllAsync(CancellationToken.None);

      // Assert
      result.Should().NotBeEmpty();
      gameList.Count.Should().Be(4);
    }
  }
}
