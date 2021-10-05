using GameStore.Application.Games.Commands.UpdateGame;
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
  public interface IGameRepository
  {
    Task<List<Game>> GetAllAsync(CancellationToken cancellationToken);
    Task<Game> GetByIdAsync(Guid id, CancellationToken cancellation);
    Task<Guid> CreateAsync(Game game, CancellationToken cancellationToken);

    Task<Unit> AddCategoryAsync(Game game, Category category, CancellationToken cancellationToken);
    Task<Unit> UpdateAsync(Game game, UpdateGameCommand request, CancellationToken cancellation);
    Task<Unit> DeleteAsync(Game game, CancellationToken cancellationToken);
  }
}
