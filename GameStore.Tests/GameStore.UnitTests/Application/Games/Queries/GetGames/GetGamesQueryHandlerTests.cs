using AutoMapper;
using FluentAssertions;
using GameStore.Application.Games.Queries.GetGames;
using GameStore.UnitTests.TestSetup;
using GameStore.UnitTests.TestSetup.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace GameStore.UnitTests.Application.Games.Queries.GetGames
{
  public class GetGamesQueryHandlerTests : IClassFixture<CommonTestFixture>
  {
    private readonly IMapper _mapper;

    public GetGamesQueryHandlerTests(CommonTestFixture testFixture)
    {
      _mapper = testFixture.Mapper;
    }

    [Fact]
    public async void Handle_ReturnsGameList()
    {
      // Arrange
      var gameRepository = MockGameRepository.GetMockGameRepository().Object;

      var handler = new GetGamesQueryHandler(gameRepository, _mapper);

      var request = new GetGamesQuery() { };

      // Act
      var result = await handler.Handle(request, CancellationToken.None);

      // Assert
      result.Should()
        .BeOfType<List<GetGamesQueryResponse>>()
        .And
        .HaveCountGreaterOrEqualTo(0);
    }
  }
}
