using GameStore.Application.Categories.Commands.UpdateCategory;
using GameStore.Application.Common.Interfaces;
using GameStore.Domain.Entities;
using GameStore.Infrastructure.Data.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameStore.Infrastructure.Data.Repositories
{
  public class CategoryRepository : ICategoryRepository
  {
    private readonly GameStoreDbContext _dbContext;
    public CategoryRepository(GameStoreDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public Task<List<Category>> GetAllAsync(CancellationToken cancellationToken)
    {
      return _dbContext.Categories
        .Include(category => category.Games)
        .ToListAsync(cancellationToken);
    }

    public async Task<Category> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
      Category category = await _dbContext.Categories
        .Include(category => category.Games)
        .SingleOrDefaultAsync(category => category.Id == id, cancellationToken);

      return category;
    }

    public async Task<Category> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
      Category category = await _dbContext.Categories.Where(category => category.Name == name)
        .Include(category => category.Games)
        .SingleOrDefaultAsync(cancellationToken);

      return category;
    }

    public async Task<Guid> CreateAsync(Category category, CancellationToken cancellationToken)
    {
      await _dbContext.Categories.AddAsync(category, cancellationToken);
      await _dbContext.SaveChangesAsync(cancellationToken);
      return category.Id;
    }

    public async Task<Unit> UpdateAsync(Category category, UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
      category.Name = string.IsNullOrWhiteSpace(request.Name) ? category.Name : request.Name;
      await _dbContext.SaveChangesAsync(cancellationToken);

      return Unit.Value;
    }

    public async Task<Unit> DeleteAsync(Category category, CancellationToken cancellationToken)
    {
      _dbContext.Categories.Remove(category);
      await _dbContext.SaveChangesAsync(cancellationToken);

      return Unit.Value;
    }
  }
}
