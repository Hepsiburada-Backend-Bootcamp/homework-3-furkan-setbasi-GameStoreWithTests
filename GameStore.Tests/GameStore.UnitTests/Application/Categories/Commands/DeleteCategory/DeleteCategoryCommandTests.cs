using FluentAssertions;
using GameStore.Application.Categories.Commands.DeleteCategory;
using GameStore.Application.Common.Exceptions;
using GameStore.Domain.Entities;
using GameStore.UnitTests.TestSetup.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace GameStore.UnitTests.Application.Categories.Commands.DeleteCategory
{
  public class DeleteCategoryCommandTests
  {
    [Fact]
    public async void Handle_WhenCommandWithExistingCategoryIdIsGiven_ShouldDeleteCategoryWithGivenId()
    {
      // Arrange
      var categoryRepository = MockCategoryRepository.GetMockCategoryRepository().Object;

      var category = new Category()
      {
        Name = Guid.NewGuid().ToString()
      };

      Guid createdCategoryId = await categoryRepository.CreateAsync(category, CancellationToken.None);

      var request = new DeleteCategoryCommand() { Id = createdCategoryId };

      var handler = new DeleteCategoryCommandHandler(categoryRepository);

      // Act
      await handler.Handle(request, CancellationToken.None);

      // Assert
      var deletedCategory = await categoryRepository.GetByIdAsync(createdCategoryId, CancellationToken.None);

      deletedCategory.Should().BeNull();
    }

    [Fact]
    public async void Handle_WhenCommandWithNonExistingCategoryIdIsGiven_ThrowsNotFoundException()
    {
      // Arrange
      var categoryRepository = MockCategoryRepository.GetMockCategoryRepository().Object;

      var request = new DeleteCategoryCommand()
      {
        Id = Guid.NewGuid()
      };

      var handler = new DeleteCategoryCommandHandler(categoryRepository);

      // Act & Assert
      FluentActions
        .Invoking(async () => await handler.Handle(request, CancellationToken.None))
        .Should().ThrowAsync<NotFoundException>().Result
        .And
        .Message.Should().Be("Category was not found");
    }
  }
}
