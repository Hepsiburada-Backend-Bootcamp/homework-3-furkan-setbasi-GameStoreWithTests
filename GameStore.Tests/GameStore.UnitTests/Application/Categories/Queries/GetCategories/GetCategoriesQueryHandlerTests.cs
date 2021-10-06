using AutoMapper;
using FluentAssertions;
using GameStore.Application.Categories.Queries.GetCategories;
using GameStore.UnitTests.TestSetup;
using GameStore.UnitTests.TestSetup.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace GameStore.UnitTests.Application.Categories.Queries.GetCategories
{
  public class GetCategoriesQueryHandlerTests : IClassFixture<CommonTestFixture>
  {
    private readonly IMapper _mapper;

    public GetCategoriesQueryHandlerTests(CommonTestFixture testFixture)
    {
      _mapper = testFixture.Mapper;
    }

    [Fact]
    public async void Handle_ReturnsCategoryList()
    {
      // Arrange
      var categoryRepository = MockCategoryRepository.GetMockCategoryRepository().Object;

      var handler = new GetCategoriesQueryHandler(categoryRepository, _mapper);

      var request = new GetCategoriesQuery() { };

      // Act
      var result = await handler.Handle(request, CancellationToken.None);

      // Assert
      result.Should()
        .BeOfType<List<GetCategoriesQueryResponse>>()
        .And
        .HaveCountGreaterOrEqualTo(0);
    }
  }
}
