using AutoMapper;
using FluentAssertions;
using GameStore.Application.Developers.Queries.GetDevelopers;
using GameStore.UnitTests.TestSetup;
using GameStore.UnitTests.TestSetup.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace GameStore.UnitTests.Application.Developers.Queries.GetDevelopers
{
  public class GetDevelopersQueryHandlerTests : IClassFixture<CommonTestFixture>
  {
    private readonly IMapper _mapper;

    public GetDevelopersQueryHandlerTests(CommonTestFixture testFixture)
    {
      _mapper = testFixture.Mapper;
    }

    [Fact]
    public async void Handle_ReturnsDeveloperList()
    {
      // Arrange
      var developerRepository = MockDeveloperRepository.GetMockDeveloperRepository().Object;

      var handler = new GetDevelopersQueryHandler(developerRepository, _mapper);

      var request = new GetDevelopersQuery() { };

      // Act
      var result = await handler.Handle(request, CancellationToken.None);

      // Assert
      result.Should()
        .BeOfType<List<GetDevelopersQueryResponse>>()
        .And
        .HaveCountGreaterOrEqualTo(0);
    }
  }
}
