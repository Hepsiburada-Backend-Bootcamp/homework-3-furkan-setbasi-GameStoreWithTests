using FluentAssertions;
using GameStore.Application.Games.Commands.UpdateGame;
using GameStore.Domain.Entities;
using GameStore.Infrastructure.Data.Contexts;
using GameStore.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace GameStore.UnitTests.Infrastructure.Data.Repositories
{
  public class GameRepositoryTests
  {

    [Fact]
    public async void GetAllAsync_ShouldReturnGameList()
    {
      // Arrange
      var dbContext = CreateDbContext();

      var game = CreateGame();
      await dbContext.Games.AddAsync(game);
      await dbContext.SaveChangesAsync();

      var gameRepository = new GameRepository(dbContext);

      // Act
      var result = await gameRepository.GetAllAsync(CancellationToken.None);

      // Assert
      result.Should().NotBeNull().And.HaveCount(1);

      await dbContext.DisposeAsync();
    }

    [Fact]
    public async void GetByIdAsync_WhenExistingIdIsGiven_ShouldReturnGameWithGivenId()
    {
      // Arrange
      var dbContext = CreateDbContext();

      var game = CreateGame();
      await dbContext.Games.AddAsync(game);
      await dbContext.SaveChangesAsync();

      var gameRepository = new GameRepository(dbContext);

      // Act
      var result = await gameRepository.GetByIdAsync(game.Id, CancellationToken.None);

      // Assert
      result.Should().NotBeNull();
      result.Name.Should().Be(game.Name);

      await dbContext.DisposeAsync();
    }

    [Fact]
    public async void AddCategoryAsync_ShouldAddCategoryToGivenGame()
    {
      // Arrange
      var dbContext = CreateDbContext();

      var game = CreateGame();
      var category = CreateCategory();

      var gameRepository = new GameRepository(dbContext);

      await dbContext.Games.AddAsync(game);
      await dbContext.Categories.AddAsync(category);
      await dbContext.SaveChangesAsync();

      var createdGame = await gameRepository.GetByIdAsync(game.Id, CancellationToken.None);

      // Act
      await gameRepository.AddCategoryAsync(createdGame, category, CancellationToken.None);

      // Assert
      var result = await gameRepository.GetByIdAsync(game.Id, CancellationToken.None);

      result.Categories.Should().NotBeNull().And.HaveCount(1).And.ContainSingle(c => c.Name == category.Name);

      await dbContext.DisposeAsync();
    }

    [Fact]
    public async void GetByIdAsync_WhenNonExistingIdIsGiven_ShouldReturnGameWithGivenId()
    {
      // Arrange
      var dbContext = CreateDbContext();

      var game = CreateGame();
      await dbContext.Games.AddAsync(game);
      await dbContext.SaveChangesAsync();

      var gameRepository = new GameRepository(dbContext);

      // Act
      Guid nonExistingGuid = Guid.NewGuid();
      var result = await gameRepository.GetByIdAsync(nonExistingGuid, CancellationToken.None);

      // Assert
      result.Should().BeNull();

      await dbContext.DisposeAsync();
    }

    [Fact]
    public async void CreateAsync_ShouldReturnCreatedGameId()
    {
      // Arrange
      var dbContext = CreateDbContext();

      var game = CreateGame();

      var gameRepository = new GameRepository(dbContext);

      // Act
      var result = await gameRepository.CreateAsync(game, CancellationToken.None);

      // Assert
      result.Should().Be(game.Id);

      await dbContext.DisposeAsync();
    }

    [Fact]
    public async void UpdateAsync_ShouldUpdateGivenGameWithGivenCommand()
    {
      // Arrange
      var dbContext = CreateDbContext();

      var game = CreateGame();

      var updateGameCommand = new UpdateGameCommand() { Name = "Updated Name" };

      var gameRepository = new GameRepository(dbContext);

      await gameRepository.CreateAsync(game, CancellationToken.None);

      // Act
      await gameRepository.UpdateAsync(game, updateGameCommand, CancellationToken.None);

      // Assert
      var updatedGame = await gameRepository.GetByIdAsync(game.Id, CancellationToken.None);

      updatedGame.Should().NotBeNull();
      updatedGame.Name.Should().Be("Updated Name");

      await dbContext.DisposeAsync();

    }

    [Fact]
    public async void DeleteAsync_ShouldDeleteGameWithGivenId()
    {
      // Arrange
      var dbContext = CreateDbContext();

      var game = CreateGame();

      var gameRepository = new GameRepository(dbContext);

      await gameRepository.CreateAsync(game, CancellationToken.None);

      // Act
      await gameRepository.DeleteAsync(game, CancellationToken.None);

      // Assert
      var deletedGame = await gameRepository.GetByIdAsync(game.Id, CancellationToken.None);

      deletedGame.Should().BeNull();

      await dbContext.DisposeAsync();
    }

    private Game CreateGame()
    {
      return new()
      {
        Id = Guid.NewGuid(),
        Name = Guid.NewGuid().ToString(),
        Price = 99,
      };
    }
    private Category CreateCategory()
    {
      return new()
      {
        Id = Guid.NewGuid(),
        Name = Guid.NewGuid().ToString()
      };
    }

    private GameStoreDbContext CreateDbContext()
    {
      var options = new DbContextOptionsBuilder<GameStoreDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString("N")).Options;
      GameStoreDbContext dbContext = new GameStoreDbContext(options);
      dbContext.Database.EnsureCreated();
      return dbContext;
    }
  }
}
