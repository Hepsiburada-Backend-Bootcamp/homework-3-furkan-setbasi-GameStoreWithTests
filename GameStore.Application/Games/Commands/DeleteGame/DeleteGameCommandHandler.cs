using GameStore.Application.Common.Exceptions;
using GameStore.Application.Common.Interfaces;
using GameStore.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameStore.Application.Games.Commands.DeleteGame
{
  public class DeleteGameCommandHandler : IRequestHandler<DeleteGameCommand>
  {
    private readonly IGameRepository _gameRepository;

    public DeleteGameCommandHandler(IGameRepository gameRepository)
    {
      _gameRepository = gameRepository;
    }

    public async Task<Unit> Handle(DeleteGameCommand request, CancellationToken cancellationToken)
    {
      Game game = await _gameRepository.GetByIdAsync(request.Id, cancellationToken);

      if(game is null)
      {
        throw new NotFoundException("Game was not found");
      }

      await _gameRepository.DeleteAsync(game, cancellationToken);

      return Unit.Value;
    }
  }
}
