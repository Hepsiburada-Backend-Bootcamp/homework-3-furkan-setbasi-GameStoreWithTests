using GameStore.Application.Common.Interfaces;
using GameStore.Application.Games.Commands.UpdateGame;
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
  public class GameRepository : IGameRepository
  {
    private readonly GameStoreDbContext _dbContext;

    public GameRepository(GameStoreDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public async Task<Guid> CreateAsync(Game game, CancellationToken cancellationToken)
    {
      await _dbContext.Games.AddAsync(game, cancellationToken);
      await _dbContext.SaveChangesAsync(cancellationToken);
      return game.Id;
    }

    public async Task<Unit> AddCategoryAsync(Game game, Category category, CancellationToken cancellationToken)
    {
      game.Categories.Add(category);
      await _dbContext.SaveChangesAsync(cancellationToken);
      return Unit.Value;
    }

    public async Task<Unit> DeleteAsync(Game game, CancellationToken cancellationToken)
    {
      _dbContext.Games.Remove(game);
      await _dbContext.SaveChangesAsync(cancellationToken);

      return Unit.Value;
    }

    public Task<List<Game>> GetAllAsync(CancellationToken cancellationToken)
    {
      return _dbContext.Games
        .Include(game => game.Developer)
        .Include(game => game.Categories)
        .ToListAsync(cancellationToken);
    }

    public async Task<Game> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
      Game game = await _dbContext.Games
        .Include(game => game.Developer)
        .Include(game => game.Categories)
        .SingleOrDefaultAsync(game => game.Id == id, cancellationToken);
      return game;
    }

    public async Task<Unit> UpdateAsync(Game game, UpdateGameCommand request, CancellationToken cancellationToken)
    {
      game.Name = string.IsNullOrWhiteSpace(request.Name) ? game.Name : request.Name;
      game.Price = request.Price == default ? game.Price : request.Price;
      game.DeveloperId = request.DeveloperId;

      await _dbContext.SaveChangesAsync(cancellationToken);

      return Unit.Value;
    }
  }
}
