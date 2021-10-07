using AutoMapper;
using FluentAssertions;
using GameStore.Application.Developers.Commands.CreateDeveloper;
using GameStore.UnitTests.TestSetup;
using GameStore.UnitTests.TestSetup.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace GameStore.UnitTests.Application.Developers.Commands.CreateDeveloper
{
  public class CreateDeveloperCommandHandlerTests : IClassFixture<CommonTestFixture>
  {
    private readonly IMapper _mapper;

    public CreateDeveloperCommandHandlerTests(CommonTestFixture testFixture)
    {
      _mapper = testFixture.Mapper;
    }

    [Fact]
    public async void Handle_ShouldReturnCreatedDeveloperId()
    {
      // Arrange
      var developerRepository = MockDeveloperRepository.GetMockDeveloperRepository().Object;

      var request = new CreateDeveloperCommand()
      {
        Name = Guid.NewGuid().ToString(),
        Country = Guid.NewGuid().ToString()
      };

      var handler = new CreateDeveloperCommandHandler(developerRepository, _mapper);

      // Act
      Guid result = await handler.Handle(request, CancellationToken.None);
      var developerList = await developerRepository.GetAllAsync(CancellationToken.None);

      // Assert
      result.Should().NotBeEmpty();
      developerList.Count.Should().Be(4);
    }
  }
}
