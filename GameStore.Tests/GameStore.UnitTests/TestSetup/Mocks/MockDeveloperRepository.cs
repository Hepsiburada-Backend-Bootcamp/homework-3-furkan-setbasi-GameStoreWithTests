using DeveloperStore.Application.Common.Interfaces;
using GameStore.Application.Developers.Commands.UpdateDeveloper;
using GameStore.Domain.Entities;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameStore.UnitTests.TestSetup.Mocks
{
  public class MockDeveloperRepository
  {
    public static Mock<IDeveloperRepository> GetMockDeveloperRepository()
    {
      List<Developer> developers = new List<Developer>()
      {
        CreateDeveloper(),CreateDeveloper(),CreateDeveloper()
      };

      var mockDeveloperRepository = new Mock<IDeveloperRepository>();

      mockDeveloperRepository.Setup(repo => repo.GetAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync(developers);

      mockDeveloperRepository
        .Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
        .ReturnsAsync(

        (Guid developerId, CancellationToken cancellationToken) => {
          return developers.Find(developer => developer.Id == developerId);
        }

        );

      mockDeveloperRepository
        .Setup(repo => repo.CreateAsync(It.IsAny<Developer>(), It.IsAny<CancellationToken>()))
        .ReturnsAsync(

        (Developer developer, CancellationToken cancellationToken) => {
          developer.Id = Guid.NewGuid();
          developers.Add(developer);
          return developer.Id;
        }

        );

      mockDeveloperRepository
        .Setup(repo => repo.UpdateAsync(It.IsAny<Developer>(), It.IsAny<UpdateDeveloperCommand>(), It.IsAny<CancellationToken>()))
        .ReturnsAsync(

        (Developer developer, UpdateDeveloperCommand request, CancellationToken cancellationToken) => {
          Developer developerToUpdate = developers.Find(c => c.Id == request.Id);

          developer.Name = string.IsNullOrWhiteSpace(request.Name) ? developer.Name : request.Name;
          developer.Country = string.IsNullOrWhiteSpace(request.Country) ? developer.Country : request.Country;

          return Unit.Value;
        }

        );

      mockDeveloperRepository
        .Setup(repo => repo.DeleteAsync(It.IsAny<Developer>(), It.IsAny<CancellationToken>()))
        .ReturnsAsync(

        (Developer developer, CancellationToken cancellationToken) => {
          developers.Remove(developer);

          return Unit.Value;
        }

        );


      return mockDeveloperRepository;
    }
    private static Developer CreateDeveloper()
    {
      return new()
      {
        Id = Guid.NewGuid(),
        Name = Guid.NewGuid().ToString()
      };
    }
  }
}
