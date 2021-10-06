using AutoMapper;
using FluentAssertions;
using GameStore.Application.Categories.Commands.CreateCategory;
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

namespace GameStore.UnitTests.Application.Categories.Commands.CreateCategory
{
  public class CreateCategoryCommandHandlerTests : IClassFixture<CommonTestFixture>
  {
    private readonly IMapper _mapper;

    public CreateCategoryCommandHandlerTests(CommonTestFixture testFixture)
    {
      _mapper = testFixture.Mapper;
    }

    [Fact]
    public async void Handle_ShouldReturnCreatedCategoryId()
    {
      // Arrange
      var categoryRepository = MockCategoryRepository.GetMockCategoryRepository().Object;

      var request = new CreateCategoryCommand()
      {
        Name = Guid.NewGuid().ToString()
      };

      var handler = new CreateCategoryCommandHandler(categoryRepository, _mapper);

      // Act
      Guid result = await handler.Handle(request, CancellationToken.None);
      var categoryList = await categoryRepository.GetAllAsync(CancellationToken.None);

      // Assert
      result.Should().NotBeEmpty();
      categoryList.Count.Should().Be(4);
    }

  }
}
