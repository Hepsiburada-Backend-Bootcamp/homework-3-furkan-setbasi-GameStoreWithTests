using GameStore.Application.Categories.Commands.CreateCategory;
using GameStore.Application.Categories.Commands.UpdateCategory;
using GameStore.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameStore.Application.Common.Interfaces
{
  public interface ICategoryRepository
  {
    Task<List<Category>> GetAllAsync(CancellationToken cancellationToken);
    Task<Category> GetByIdAsync(Guid id, CancellationToken cancellation);
    Task<Category> GetByNameAsync(string name, CancellationToken cancellationToken);
    Task<Guid> CreateAsync(Category category, CancellationToken cancellationToken);
    Task<Unit> UpdateAsync(Category category, UpdateCategoryCommand request, CancellationToken cancellation);
    Task<Unit> DeleteAsync(Category category, CancellationToken cancellationToken);
  }
}
