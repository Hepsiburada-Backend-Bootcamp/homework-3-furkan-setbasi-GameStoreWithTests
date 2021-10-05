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

namespace GameStore.Application.Games.Commands.UpdateGame
{
  public class UpdateGameCommandHandler : IRequestHandler<UpdateGameCommand, Unit>
  {
    private readonly IGameRepository _gameRepository;

    public UpdateGameCommandHandler(IGameRepository gameRepository)
    {
      _gameRepository = gameRepository;
    }

    public async Task<Unit> Handle(UpdateGameCommand request, CancellationToken cancellationToken)
    {
      Game gameToUpdate = await _gameRepository.GetByIdAsync(request.Id, cancellationToken);

      if(gameToUpdate is null)
      {
        throw new NotFoundException("Game was not found.");
      }

      await _gameRepository.UpdateAsync(gameToUpdate, request, cancellationToken);

      return Unit.Value;

    }
  }
}
