using AutoMapper;
using FluentAssertions;
using GameStore.Application.Games.Queries.GetGameById;
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

namespace GameStore.UnitTests.Application.Games.Queries.GetGameById
{
  public class GetGameByIdQueryHandlerTests : IClassFixture<CommonTestFixture>
  {
    private readonly IMapper _mapper;
    public GetGameByIdQueryHandlerTests(CommonTestFixture testFixture)
    {
      _mapper = testFixture.Mapper;
    }

    [Fact]
    public async void Handle_WhenQueryWithExistingGameIdIsGiven_ShouldReturnGameWithGivenId()
    {
      // Arrange
      var gameRepository = MockGameRepository.GetMockGameRepository().Object;

      Game game = new()
      {
        Id = Guid.NewGuid(),
        Name = Guid.NewGuid().ToString(),
        Price = 99
      };

      Guid createdGameId = await gameRepository.CreateAsync(game, CancellationToken.None);


      GetGameByIdQuery request = new GetGameByIdQuery()
      {
        Id = createdGameId,
      };

      GetGameByIdQueryHandler handler = new(gameRepository, _mapper);

      // Act
      var result = await handler.Handle(request, CancellationToken.None);

      // Assert
      result.Id.Should().Be(createdGameId);
      result.Name.Should().Be(game.Name);
      result.Price.Should().Be(game.Price);
    }

  }
}
