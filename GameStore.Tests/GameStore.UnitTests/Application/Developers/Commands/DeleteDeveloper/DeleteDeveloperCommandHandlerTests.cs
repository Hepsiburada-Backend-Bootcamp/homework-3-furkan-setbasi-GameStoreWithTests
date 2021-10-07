using FluentAssertions;
using GameStore.Application.Common.Exceptions;
using GameStore.Application.Developers.Commands.DeleteDeveloper;
using GameStore.Domain.Entities;
using GameStore.UnitTests.TestSetup.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace GameStore.UnitTests.Application.Developers.Commands.DeleteDeveloper
{
  public class DeleteDeveloperCommandHandlerTests
  {
    [Fact]
    public async void Handle_WhenCommandWithExistingDeveloperIdIsGiven_ShouldDeleteDeveloperWithGivenId()
    {
      // Arrange
      var developerRepository = MockDeveloperRepository.GetMockDeveloperRepository().Object;

      var developer = new Developer()
      {
        Name = Guid.NewGuid().ToString()
      };

      Guid createdDeveloperId = await developerRepository.CreateAsync(developer, CancellationToken.None);

      var request = new DeleteDeveloperCommand() { Id = createdDeveloperId };

      var handler = new DeleteDeveloperCommandHandler(developerRepository);

      // Act
      await handler.Handle(request, CancellationToken.None);

      // Assert
      var deletedDeveloper = await developerRepository.GetByIdAsync(createdDeveloperId, CancellationToken.None);

      deletedDeveloper.Should().BeNull();
    }

    [Fact]
    public void Handle_WhenCommandWithNonExistingDeveloperIdIsGiven_ThrowsNotFoundException()
    {
      // Arrange
      var developerRepository = MockDeveloperRepository.GetMockDeveloperRepository().Object;

      var request = new DeleteDeveloperCommand()
      {
        Id = Guid.NewGuid()
      };

      var handler = new DeleteDeveloperCommandHandler(developerRepository);

      // Act & Assert
      FluentActions
        .Invoking(async () => await handler.Handle(request, CancellationToken.None))
        .Should().ThrowAsync<NotFoundException>().Result
        .And
        .Message.Should().Be("Developer was not found");
    }
  }
}
