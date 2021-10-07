using FluentAssertions;
using GameStore.Application.Common.Exceptions;
using GameStore.Application.Games.Commands.AddCategory;
using GameStore.Domain.Entities;
using GameStore.UnitTests.TestSetup.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace GameStore.UnitTests.Application.Games.Commands.AddCategory
{
  public class AddCategoryToGameCommandHandlerTests
  {
    [Fact]
    public void Handle_WhenCommandWithNonExistingGameIdIsGiven_ThrowsNotFoundException()
    {
      // Arrange
      var gameRepository = MockGameRepository.GetMockGameRepository().Object;
      var categoryRepository = MockCategoryRepository.GetMockCategoryRepository().Object;

      var request = new AddCategoryToGameCommand()
      {
        Id = Guid.NewGuid(),
        Name = Guid.NewGuid().ToString()
      };

      var handler = new AddCategoryToGameCommandHandler(categoryRepository, gameRepository);

      // Act & Assert
      FluentActions
        .Invoking(async () => await handler.Handle(request, CancellationToken.None))
        .Should().ThrowAsync<NotFoundException>().Result
        .And
        .Message.Should().Be("Game was not found");
    }

    [Fact]
    public async void Handle_WhenCommandWithNonExistingCategoryNameIsGiven_ShouldCreateNewCategoryAndAddItToGameWithGivenId()
    {
      // Arrange
      var gameRepository = MockGameRepository.GetMockGameRepository().Object;
      var categoryRepository = MockCategoryRepository.GetMockCategoryRepository().Object;

      Game game = new Game()
      {
        Name = Guid.NewGuid().ToString(),
        Price = 50,
        Categories = new List<Category>()
      };

      var createdGameId = await gameRepository.CreateAsync(game, CancellationToken.None);

      var request = new AddCategoryToGameCommand()
      {
        Id = createdGameId,
        Name = Guid.NewGuid().ToString()
      };

      var handler = new AddCategoryToGameCommandHandler(categoryRepository, gameRepository);

      // Act
      await handler.Handle(request, CancellationToken.None);

      // Assert
      var categories = await categoryRepository.GetAllAsync(CancellationToken.None);
      var createdCategory = categories.Find(c => c.Name == request.Name);

      var gameWithAddedCategory = await gameRepository.GetByIdAsync(createdGameId, CancellationToken.None);

      categories
        .Should().BeOfType<List<Category>>()
        .And.NotBeEmpty()
        .And.NotBeNull()
        .And.HaveCount(4);

      createdCategory.Name
        .Should().Be(request.Name);

      gameWithAddedCategory.Categories
        .Should().BeOfType<List<Category>>()
        .And.NotBeNull()
        .And.NotBeEmpty()
        .And.HaveCount(1);

      gameWithAddedCategory.Categories.ToList()[0]
        .Name.Should().Be(createdCategory.Name);

      gameWithAddedCategory.Categories.ToList()[0]
        .Id.Should().Be(createdCategory.Id);
    }

    [Fact]
    public async void Handle_WhenCommandWithExistingCategoryNameIsGiven_ShouldAddItToGameWithGivenId()
    {
      // Arrange
      var gameRepository = MockGameRepository.GetMockGameRepository().Object;
      var categoryRepository = MockCategoryRepository.GetMockCategoryRepository().Object;

      Game existingGame = (await gameRepository.GetAllAsync(CancellationToken.None))[0];
      Category existingCategory = (await categoryRepository.GetAllAsync(CancellationToken.None))[0];

      var request = new AddCategoryToGameCommand()
      {
        Id = existingGame.Id,
        Name = existingCategory.Name
      };

      var handler = new AddCategoryToGameCommandHandler(categoryRepository, gameRepository);

      // Act
      await handler.Handle(request, CancellationToken.None);

      // Assert
      var categories = await categoryRepository.GetAllAsync(CancellationToken.None);

      categories
        .Should().BeOfType<List<Category>>()
        .And.NotBeEmpty()
        .And.NotBeNull()
        .And.HaveCount(3);

      existingGame.Categories.ToList()[0]
        .Name.Should().Be(existingCategory.Name);

      existingGame.Categories.ToList()[0]
        .Id.Should().Be(existingCategory.Id);
    }
  }
}
