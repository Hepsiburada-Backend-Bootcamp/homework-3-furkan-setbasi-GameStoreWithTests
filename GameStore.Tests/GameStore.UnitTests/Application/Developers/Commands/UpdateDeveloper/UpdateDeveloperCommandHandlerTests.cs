using AutoMapper;
using FluentAssertions;
using GameStore.Application.Common.Exceptions;
using GameStore.Application.Developers.Commands.UpdateDeveloper;
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

namespace GameStore.UnitTests.Application.Developers.Commands.UpdateDeveloper
{
  public class UpdateDeveloperCommandHandlerTests : IClassFixture<CommonTestFixture>
  {
    private readonly IMapper _mapper;
    public UpdateDeveloperCommandHandlerTests(CommonTestFixture testFixture)
    {
      _mapper = testFixture.Mapper;
    }

    [Fact]
    public async void Handle_WhenCommandWithExistingDeveloperIdIsGiven_ShouldUpdateDeveloperWithGivenId()
    {
      // Arrange
      var developerRepository = MockDeveloperRepository.GetMockDeveloperRepository().Object;

      var developer = new Developer()
      {
        Name = Guid.NewGuid().ToString()
      };

      Guid createdDeveloperId = await developerRepository.CreateAsync(developer, CancellationToken.None);

      var request = new UpdateDeveloperCommand()
      {
        Id = createdDeveloperId,
        Name = Guid.NewGuid().ToString()
      };

      var handler = new UpdateDeveloperCommandHandler(developerRepository);

      // Act
      await handler.Handle(request, CancellationToken.None);

      // Assert
      var updatedDeveloper = await developerRepository.GetByIdAsync(createdDeveloperId, CancellationToken.None);

      updatedDeveloper.Name.Should().Be(request.Name);
    }

    [Fact]
    public void Handle_WhenCommandWithNonExistingDeveloperIdIsGiven_ThrowsNotFoundException()
    {
      // Arrange
      var developerRepository = MockDeveloperRepository.GetMockDeveloperRepository().Object;

      var request = new UpdateDeveloperCommand()
      {
        Id = Guid.NewGuid(),
        Name = Guid.NewGuid().ToString()
      };

      var handler = new UpdateDeveloperCommandHandler(developerRepository);

      // Act & Assert
      FluentActions
        .Invoking(async () => await handler.Handle(request, CancellationToken.None))
        .Should()
        .ThrowAsync<NotFoundException>()
        .Result.And.Message.Should().Be("Developer was not found");
    }

    [Fact]
    public async void Handle_WhenCommandWithEmptyDeveloperNameIsGiven_ShouldNotUpdateDeveloperName()
    {
      // Arrange
      var developerRepository = MockDeveloperRepository.GetMockDeveloperRepository().Object;

      var developer = new Developer()
      {
        Name = Guid.NewGuid().ToString()
      };

      Guid createdDeveloperId = await developerRepository.CreateAsync(developer, CancellationToken.None);

      var request = new UpdateDeveloperCommand()
      {
        Id = createdDeveloperId,
        Name = ""
      };

      var handler = new UpdateDeveloperCommandHandler(developerRepository);

      // Act
      await handler.Handle(request, CancellationToken.None);

      // Assert
      var updatedDeveloper = await developerRepository.GetByIdAsync(createdDeveloperId, CancellationToken.None);

      updatedDeveloper.Name.Should().Be(developer.Name);
    }
  }
}
