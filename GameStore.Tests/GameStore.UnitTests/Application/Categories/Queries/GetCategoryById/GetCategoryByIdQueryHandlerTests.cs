using AutoMapper;
using FluentAssertions;
using GameStore.Application.Categories.Queries.GetCategoryById;
using GameStore.Application.Common.Exceptions;
using GameStore.Application.Common.Interfaces;
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

namespace GameStore.UnitTests.Application.Categories.Queries.GetCategoryById
{
  public class GetCategoryByIdQueryHandlerTests : IClassFixture<CommonTestFixture>
  {
    private readonly IMapper _mapper;
    public GetCategoryByIdQueryHandlerTests(CommonTestFixture testFixture)
    {
      _mapper = testFixture.Mapper;
    }

    [Fact]
    public async void Handle_WhenQueryWithExistingCategoryIdIsGiven_ShouldReturnCategoryWithGivenId()
    {
      // Arrange
      var categoryRepository = MockCategoryRepository.GetMockCategoryRepository().Object;

      Category category = new()
      {
        Id = Guid.NewGuid(),
        Name = Guid.NewGuid().ToString()
      };

      Guid createdCategoryId = await categoryRepository.CreateAsync(category, CancellationToken.None);


      GetCategoryByIdQuery request = new GetCategoryByIdQuery()
      {
        Id = createdCategoryId,
      };

      GetCategoryByIdQueryHandler handler = new(categoryRepository, _mapper);

      // Act
      var result = await handler.Handle(request, CancellationToken.None);

      // Assert
      result.Id.Should().Be(createdCategoryId);
      result.Name.Should().Be(category.Name);
    }

    [Fact]
    public void Handle_WhenQueryWithNonExistingCategoryIdIsGiven_ThrowsNotFoundException()
    {
      // Arrange
      var categoryRepository = MockCategoryRepository.GetMockCategoryRepository().Object;

      GetCategoryByIdQuery request = new GetCategoryByIdQuery()
      {
        Id = Guid.NewGuid(),
      };

      GetCategoryByIdQueryHandler handler = new(categoryRepository, _mapper);

      // Act & Assert
      FluentActions
        .Invoking(async () => await handler.Handle(request, CancellationToken.None))
        .Should()
        .ThrowAsync<NotFoundException>()
        .Result.And.Message.Should().Be("Category was not found");
    }
  }
}
