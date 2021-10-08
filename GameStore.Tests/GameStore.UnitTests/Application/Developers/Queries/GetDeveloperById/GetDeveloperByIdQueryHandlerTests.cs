using AutoMapper;
using FluentAssertions;
using GameStore.Application.Common.Exceptions;
using GameStore.Application.Developers.Queries.GetDeveloperById;
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

namespace GameStore.UnitTests.Application.Developers.Queries.GetDeveloperById
{
  public class GetDeveloperByIdQueryHandlerTests : IClassFixture<CommonTestFixture>
  {
    private readonly IMapper _mapper;
    public GetDeveloperByIdQueryHandlerTests(CommonTestFixture testFixture)
    {
      _mapper = testFixture.Mapper;
    }

    [Fact]
    public async void Handle_WhenQueryWithExistingDeveloperIdIsGiven_ShouldReturnDeveloperWithGivenId()
    {
      // Arrange
      var developerRepository = MockDeveloperRepository.GetMockDeveloperRepository().Object;

      Developer developer = new()
      {
        Id = Guid.NewGuid(),
        Name = Guid.NewGuid().ToString()
      };

      Guid createdDeveloperId = await developerRepository.CreateAsync(developer, CancellationToken.None);


      GetDeveloperByIdQuery request = new GetDeveloperByIdQuery()
      {
        Id = createdDeveloperId,
      };

      GetDeveloperByIdQueryHandler handler = new(developerRepository, _mapper);

      // Act
      var result = await handler.Handle(request, CancellationToken.None);

      // Assert
      result.Id.Should().Be(createdDeveloperId);
      result.Name.Should().Be(developer.Name);
    }

    [Fact]
    public void Handle_WhenQueryWithNonExistingDeveloperIdIsGiven_ThrowsNotFoundException()
    {
      // Arrange
      var developerRepository = MockDeveloperRepository.GetMockDeveloperRepository().Object;

      GetDeveloperByIdQuery request = new GetDeveloperByIdQuery()
      {
        Id = Guid.NewGuid(),
      };

      GetDeveloperByIdQueryHandler handler = new(developerRepository, _mapper);

      // Act & Assert
      FluentActions
        .Invoking(async () => await handler.Handle(request, CancellationToken.None))
        .Should()
        .ThrowAsync<NotFoundException>()
        .Result.And.Message.Should().Be("Developer was not found");
    }
  }
}
