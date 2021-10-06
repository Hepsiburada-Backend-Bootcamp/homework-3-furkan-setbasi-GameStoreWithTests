using AutoMapper;
using FluentAssertions;
using GameStore.Application.Categories.Commands.UpdateCategory;
using GameStore.Application.Common.Exceptions;
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

namespace GameStore.UnitTests.Application.Categories.Commands.UpdateCategory
{
  public class UpdateCategoryCommandTests : IClassFixture<CommonTestFixture>
  {
    private readonly IMapper _mapper;
    public UpdateCategoryCommandTests(CommonTestFixture testFixture)
    {
      _mapper = testFixture.Mapper;
    }

    [Fact]
    public async void Handle_WhenCommandWithExistingCategoryIdIsGiven_ShouldUpdateCategoryWithGivenId()
    {
      // Arrange
      var categoryRepository = MockCategoryRepository.GetMockCategoryRepository().Object;

      var category = new Category()
      {
        Name = Guid.NewGuid().ToString()
      };

      Guid createdCategoryId = await categoryRepository.CreateAsync(category, CancellationToken.None);

      var request = new UpdateCategoryCommand()
      {
        Id = createdCategoryId,
        Name = Guid.NewGuid().ToString()
      };

      var handler = new UpdateCategoryCommandHandler(categoryRepository);

      // Act
      await handler.Handle(request, CancellationToken.None);

      // Assert
      var updatedCategory = await categoryRepository.GetByIdAsync(createdCategoryId, CancellationToken.None);

      updatedCategory.Name.Should().Be(request.Name);
    }

    [Fact]
    public void Handle_WhenCommandWithNonExistingCategoryIdIsGiven_ThrowsNotFoundException()
    {
      // Arrange
      var categoryRepository = MockCategoryRepository.GetMockCategoryRepository().Object;

      var request = new UpdateCategoryCommand()
      {
        Id = Guid.NewGuid(),
        Name = Guid.NewGuid().ToString()
      };

      var handler = new UpdateCategoryCommandHandler(categoryRepository);

      // Act & Assert
      FluentActions
        .Invoking(async () => await handler.Handle(request, CancellationToken.None))
        .Should()
        .ThrowAsync<NotFoundException>()
        .Result.And.Message.Should().Be("Category was not found");
    }

    [Fact]
    public async void Handle_WhenCommandWithEmptyCategoryNameIsGiven_ShouldNotUpdateCategoryName()
    {
      // Arrange
      var categoryRepository = MockCategoryRepository.GetMockCategoryRepository().Object;

      var category = new Category()
      {
        Name = Guid.NewGuid().ToString()
      };

      Guid createdCategoryId = await categoryRepository.CreateAsync(category, CancellationToken.None);

      var request = new UpdateCategoryCommand()
      {
        Id = createdCategoryId,
        Name = ""
      };

      var handler = new UpdateCategoryCommandHandler(categoryRepository);

      // Act
      await handler.Handle(request, CancellationToken.None);

      // Assert
      var updatedCategory = await categoryRepository.GetByIdAsync(createdCategoryId, CancellationToken.None);

      updatedCategory.Name.Should().Be(category.Name);
    }
  }
}
