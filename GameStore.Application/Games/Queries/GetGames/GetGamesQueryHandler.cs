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

namespace GameStore.Application.Games.Queries.GetGames
{
  public class GetGamesQueryHandler : IRequestHandler<GetGamesQuery, List<GetGamesQueryResponse>>
  {
    private readonly IGameRepository _gameRepository;
    private readonly IMapper _mapper;

    public GetGamesQueryHandler(IGameRepository gameRepository, IMapper mapper)
    {
      _gameRepository = gameRepository;
      _mapper = mapper;
    }

    public async Task<List<GetGamesQueryResponse>> Handle(GetGamesQuery request, CancellationToken cancellationToken)
    {
      List<Game> games = await _gameRepository.GetAllAsync(cancellationToken);

      List<GetGamesQueryResponse> gamesResponse = _mapper.Map<List<GetGamesQueryResponse>>(games);

      return gamesResponse;
    }
  }
}
