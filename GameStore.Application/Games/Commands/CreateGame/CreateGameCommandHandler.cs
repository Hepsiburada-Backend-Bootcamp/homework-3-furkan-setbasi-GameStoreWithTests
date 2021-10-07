using AutoMapper;
using GameStore.Application.Common.Interfaces;
using GameStore.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameStore.Application.Games.Commands.CreateGame
{
  public class CreateGameCommandHandler : IRequestHandler<CreateGameCommand, Guid>
  {
    private readonly IGameRepository _gameRepository;
    private readonly IMapper _mapper;

    public CreateGameCommandHandler(IGameRepository gameRepository, IMapper mapper)
    {
      _gameRepository = gameRepository;
      _mapper = mapper;
    }

    public async Task<Guid> Handle(CreateGameCommand request, CancellationToken cancellationToken)
    {
      Game newGame = _mapper.Map<Game>(request);

      Guid createdGameId = await _gameRepository.CreateAsync(newGame, cancellationToken);
      return createdGameId;
    }
  }
}
