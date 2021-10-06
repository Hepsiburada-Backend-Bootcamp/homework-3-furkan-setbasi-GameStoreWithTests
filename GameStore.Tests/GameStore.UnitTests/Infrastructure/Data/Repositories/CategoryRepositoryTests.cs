using FluentAssertions;
using GameStore.Application.Categories.Commands.UpdateCategory;
using GameStore.Domain.Entities;
using GameStore.Infrastructure.Data.Contexts;
using GameStore.Infrastructure.Data.Repositories;
using GameStore.UnitTests.TestSetup;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace GameStore.UnitTests.Infrastructure.Data.Repositories
{
  public class CategoryRepositoryTests : IClassFixture<CommonTestFixture>
  {
    //private readonly GameStoreDbContext dbContext;

    public CategoryRepositoryTests(CommonTestFixture testFixture)
    {
      //dbContext = testFixture.Context;
    }

    [Fact]
    public async void GetAllAsync_ShouldReturnCategoryList()
    {
      // Arrange
      var dbContext = CreateDbContext();

      var category = CreateCategory();
      await dbContext.Categories.AddAsync(category);
      await dbContext.SaveChangesAsync();

      var categoryRepository = new CategoryRepository(dbContext);

      // Act
      var result = await categoryRepository.GetAllAsync(CancellationToken.None);

      // Assert
      result.Should().NotBeNull().And.HaveCount(1);

      await dbContext.DisposeAsync();
    }

    [Fact]
    public async void GetByIdAsync_WhenExistingIdIsGiven_ShouldReturnCategoryWithGivenId()
    {
      // Arrange
      var dbContext = CreateDbContext();

      var category = CreateCategory();
      await dbContext.Categories.AddAsync(category);
      await dbContext.SaveChangesAsync();

      var categoryRepository = new CategoryRepository(dbContext);

      // Act
      var result = await categoryRepository.GetByIdAsync(category.Id, CancellationToken.None);

      // Assert
      result.Should().NotBeNull();
      result.Name.Should().Be(category.Name);

      await dbContext.DisposeAsync();
    }

    [Fact]
    public async void GetByIdAsync_WhenNonExistingIdIsGiven_ShouldReturnCategoryWithGivenId()
    {
      // Arrange
      var dbContext = CreateDbContext();

      var category = CreateCategory();
      await dbContext.Categories.AddAsync(category);
      await dbContext.SaveChangesAsync();

      var categoryRepository = new CategoryRepository(dbContext);

      // Act
      Guid nonExistingGuid = Guid.NewGuid();
      var result = await categoryRepository.GetByIdAsync(nonExistingGuid, CancellationToken.None);

      // Assert
      result.Should().BeNull();

      await dbContext.DisposeAsync();
    }

    [Fact]
    public async void GetByNameAsync_WhenExistingNameIsGiven_ShouldReturnCategoryWithGivenName()
    {
      // Arrange
      var dbContext = CreateDbContext();

      var category = CreateCategory();
      await dbContext.Categories.AddAsync(category);
      await dbContext.SaveChangesAsync();

      var categoryRepository = new CategoryRepository(dbContext);

      // Act
      var result = await categoryRepository.GetByNameAsync(category.Name, CancellationToken.None);

      // Assert
      result.Should().NotBeNull();
      result.Name.Should().Be(category.Name);

      await dbContext.DisposeAsync();
    }

    [Fact]
    public async void GetByNameAsync_WhenNonExistingNameIsGiven_WhenExistingNameIsGiven_ShouldReturnCategoryWithGivenName()
    {
      // Arrange
      var dbContext = CreateDbContext();

      var category = CreateCategory();
      await dbContext.Categories.AddAsync(category);
      await dbContext.SaveChangesAsync();

      var categoryRepository = new CategoryRepository(dbContext);

      // Act
      string nonExistingName = "Non Existing Name";
      var result = await categoryRepository.GetByNameAsync(nonExistingName, CancellationToken.None);

      // Assert
      result.Should().BeNull();

      await dbContext.DisposeAsync();
    }

    [Fact]
    public async void CreateAsync_ShouldReturnCreatedCategoryId()
    {
      // Arrange
      var dbContext = CreateDbContext();

      var category = CreateCategory();

      var categoryRepository = new CategoryRepository(dbContext);

      // Act
      var result = await categoryRepository.CreateAsync(category, CancellationToken.None);

      // Assert
      result.Should().Be(category.Id);

      await dbContext.DisposeAsync();
    }

    [Fact]
    public async void UpdateAsync_ShouldUpdateGivenCategoryWithGivenCommand()
    {
      // Arrange
      var dbContext = CreateDbContext();

      var category = CreateCategory();

      var updateCategoryCommand = new UpdateCategoryCommand() { Name = "Updated Name" };

      var categoryRepository = new CategoryRepository(dbContext);

      Guid createdCategoryId = await categoryRepository.CreateAsync(category, CancellationToken.None);

      // Act
      await categoryRepository.UpdateAsync(category, updateCategoryCommand, CancellationToken.None);

      // Assert
      var updatedCategory = await categoryRepository.GetByIdAsync(createdCategoryId, CancellationToken.None);

      updatedCategory.Should().NotBeNull();
      updatedCategory.Name.Should().Be("Updated Name");

      await dbContext.DisposeAsync();
    }

    [Fact]
    public async void DeleteAsync_ShouldDeleteCategoryWithGivenId()
    {
      // Arrange
      var dbContext = CreateDbContext();

      var category = CreateCategory();

      var categoryRepository = new CategoryRepository(dbContext);

      await categoryRepository.CreateAsync(category, CancellationToken.None);

      // Act
      await categoryRepository.DeleteAsync(category, CancellationToken.None);

      // Assert
      var deletedCategory = await categoryRepository.GetByIdAsync(category.Id, CancellationToken.None);

      deletedCategory.Should().BeNull();

      await dbContext.DisposeAsync();
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
