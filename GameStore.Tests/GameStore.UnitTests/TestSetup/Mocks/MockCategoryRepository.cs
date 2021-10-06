using GameStore.Application.Categories.Commands.UpdateCategory;
using GameStore.Application.Common.Interfaces;
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
  public static class MockCategoryRepository
  {
    public static Mock<ICategoryRepository> GetMockCategoryRepository()
    {
      List<Category> categories = new List<Category>()
      {
        CreateCategory(),CreateCategory(),CreateCategory()
      };

      var mockCategoryRepository = new Mock<ICategoryRepository>();

      mockCategoryRepository.Setup(repo => repo.GetAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync(categories);

      mockCategoryRepository
        .Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
        .ReturnsAsync(

        (Guid categoryId, CancellationToken cancellationToken) => {
          return categories.Find(category => category.Id == categoryId);
        }

        );

      mockCategoryRepository
        .Setup(repo => repo.CreateAsync(It.IsAny<Category>(), It.IsAny<CancellationToken>()))
        .ReturnsAsync(

        (Category category, CancellationToken cancellationToken) => {
          category.Id = Guid.NewGuid();
          categories.Add(category);
          return category.Id;
        }

        );

      mockCategoryRepository
        .Setup(repo => repo.UpdateAsync(It.IsAny<Category>(), It.IsAny<UpdateCategoryCommand>(), It.IsAny<CancellationToken>()))
        .ReturnsAsync(

        (Category category, UpdateCategoryCommand request, CancellationToken cancellationToken) => {
          Category categoryToUpdate = categories.Find(c => c.Id == request.Id);

          category.Name = string.IsNullOrWhiteSpace(request.Name) ? category.Name : request.Name;

          return Unit.Value;
        }

        );

      mockCategoryRepository
        .Setup(repo => repo.DeleteAsync(It.IsAny<Category>(), It.IsAny<CancellationToken>()))
        .ReturnsAsync(

        (Category category, CancellationToken cancellationToken) => {
          categories.Remove(category);

          return Unit.Value;
        }

        );


      return mockCategoryRepository;
    }

    private static Category CreateCategory()
    {
      return new()
      {
        Id = Guid.NewGuid(),
        Name = Guid.NewGuid().ToString()
      };
    }
  }
}
