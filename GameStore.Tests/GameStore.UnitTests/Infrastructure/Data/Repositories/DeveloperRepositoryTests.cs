using FluentAssertions;
using GameStore.Application.Developers.Commands.UpdateDeveloper;
using GameStore.Domain.Entities;
using GameStore.Infrastructure.Data.Contexts;
using GameStore.Infrastructure.Data.Repositories;
using GameStore.UnitTests.TestSetup;
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
  public class DeveloperRepositoryTests : IClassFixture<CommonTestFixture>
  {
    private readonly GameStoreDbContext dbContext;

    public DeveloperRepositoryTests(CommonTestFixture testFixture)
    {
      dbContext = testFixture.Context;
    }

    [Fact]
    public async void GetAllAsync_ShouldReturnDeveloperList()
    {
      // Arrange
      var dbContext = CreateDbContext();

      var developer = CreateDeveloper();
      await dbContext.Developers.AddAsync(developer);
      await dbContext.SaveChangesAsync();

      var developerRepository = new DeveloperRepository(dbContext);

      // Act
      var result = await developerRepository.GetAllAsync(CancellationToken.None);

      // Assert
      result.Should().NotBeNull().And.HaveCount(1);

      await dbContext.DisposeAsync();
    }

    [Fact]
    public async void GetByIdAsync_WhenExistingIdIsGiven_ShouldReturnDeveloperWithGivenId()
    {
      // Arrange
      var dbContext = CreateDbContext();

      var developer = CreateDeveloper();
      await dbContext.Developers.AddAsync(developer);
      await dbContext.SaveChangesAsync();

      var developerRepository = new DeveloperRepository(dbContext);

      // Act
      var result = await developerRepository.GetByIdAsync(developer.Id, CancellationToken.None);

      // Assert
      result.Should().NotBeNull();
      result.Name.Should().Be(developer.Name);

      await dbContext.DisposeAsync();
    }

    [Fact]
    public async void GetByIdAsync_WhenNonExistingIdIsGiven_ShouldReturnDeveloperWithGivenId()
    {
      // Arrange
      var dbContext = CreateDbContext();

      var developer = CreateDeveloper();
      await dbContext.Developers.AddAsync(developer);
      await dbContext.SaveChangesAsync();

      var developerRepository = new DeveloperRepository(dbContext);

      // Act
      Guid nonExistingGuid = Guid.NewGuid();
      var result = await developerRepository.GetByIdAsync(nonExistingGuid, CancellationToken.None);

      // Assert
      result.Should().BeNull();

      await dbContext.DisposeAsync();
    }

    [Fact]
    public async void CreateAsync_ShouldReturnCreatedDeveloperId()
    {
      // Arrange
      var dbContext = CreateDbContext();

      var developer = CreateDeveloper();

      var developerRepository = new DeveloperRepository(dbContext);

      // Act
      var result = await developerRepository.CreateAsync(developer, CancellationToken.None);

      // Assert
      result.Should().Be(developer.Id);

      await dbContext.DisposeAsync();
    }

    [Fact]
    public async void UpdateAsync_ShouldUpdateGivenDeveloperWithGivenCommand()
    {
      // Arrange
      var dbContext = CreateDbContext();

      var developer = CreateDeveloper();

      var updateDeveloperCommand = new UpdateDeveloperCommand() { Name = "Updated Name" };

      var developerRepository = new DeveloperRepository(dbContext);

      await developerRepository.CreateAsync(developer, CancellationToken.None);

      // Act
      await developerRepository.UpdateAsync(developer, updateDeveloperCommand, CancellationToken.None);

      // Assert
      var updatedDeveloper = await developerRepository.GetByIdAsync(developer.Id, CancellationToken.None);

      updatedDeveloper.Should().NotBeNull();
      updatedDeveloper.Name.Should().Be("Updated Name");

      await dbContext.DisposeAsync();

    }

    [Fact]
    public async void DeleteAsync_ShouldDeleteDeveloperWithGivenId()
    {
      // Arrange
      var dbContext = CreateDbContext();

      var developer = CreateDeveloper();

      var developerRepository = new DeveloperRepository(dbContext);

      await developerRepository.CreateAsync(developer, CancellationToken.None);

      // Act
      await developerRepository.DeleteAsync(developer, CancellationToken.None);

      // Assert
      var deletedDeveloper = await developerRepository.GetByIdAsync(developer.Id, CancellationToken.None);

      deletedDeveloper.Should().BeNull();

      await dbContext.DisposeAsync();
    }

    private Developer CreateDeveloper()
    {
      return new()
      {
        Id = Guid.NewGuid(),
        Name = Guid.NewGuid().ToString(),
        Country = Guid.NewGuid().ToString(),
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
