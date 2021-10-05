using AutoMapper;
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

namespace GameStore.Application.Games.Queries.GetGameById
{
  public class GetGameByIdQueryHandler : IRequestHandler<GetGameByIdQuery, GetGameByIdQueryResponse>
  {
    private readonly IGameRepository _gameRepository;
    private readonly IMapper _mapper;

    public GetGameByIdQueryHandler(IGameRepository gameRepository, IMapper mapper)
    {
      _gameRepository = gameRepository;
      _mapper = mapper;
    }

    public async Task<GetGameByIdQueryResponse> Handle(GetGameByIdQuery request, CancellationToken cancellationToken)
    {
      Game game = await _gameRepository.GetByIdAsync(request.Id, cancellationToken);

      if(game is null)
      {
        throw new NotFoundException("Game was not found");
      }

      return _mapper.Map<GetGameByIdQueryResponse>(game);
    }
  }
}
