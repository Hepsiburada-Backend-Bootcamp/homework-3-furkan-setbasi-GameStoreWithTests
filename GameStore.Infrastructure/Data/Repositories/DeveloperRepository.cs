using DeveloperStore.Application.Common.Interfaces;
using GameStore.Application.Developers.Commands.UpdateDeveloper;
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
  public class DeveloperRepository : IDeveloperRepository
  {
    private readonly GameStoreDbContext _dbContext;

    public DeveloperRepository(GameStoreDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public async Task<Guid> CreateAsync(Developer developer, CancellationToken cancellationToken)
    {
      await _dbContext.Developers.AddAsync(developer, cancellationToken);
      await _dbContext.SaveChangesAsync(cancellationToken);
      return developer.Id;
    }

    public async Task<Unit> DeleteAsync(Developer developer, CancellationToken cancellationToken)
    {
      _dbContext.Developers.Remove(developer);
      await _dbContext.SaveChangesAsync(cancellationToken);

      return Unit.Value;
    }

    public Task<List<Developer>> GetAllAsync(CancellationToken cancellationToken)
    {
      return _dbContext.Developers
        .Include(developer => developer.Games)
        .ThenInclude(game => game.Categories)
        .ToListAsync(cancellationToken);
    }

    public async Task<Developer> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
      Developer developer = await _dbContext.Developers
        .Include(developer => developer.Games)
        .ThenInclude(game => game.Categories)
        .SingleOrDefaultAsync(developer => developer.Id == id, cancellationToken);
      return developer;
    }

    public async Task<Unit> UpdateAsync(Developer developer, UpdateDeveloperCommand request, CancellationToken cancellationToken)
    {
      developer.Name = string.IsNullOrWhiteSpace(request.Name) ? developer.Name : request.Name;
      developer.Country = string.IsNullOrWhiteSpace(request.Country) ? developer.Country : request.Country;
      await _dbContext.SaveChangesAsync(cancellationToken);

      return Unit.Value;
    }
  }
}
