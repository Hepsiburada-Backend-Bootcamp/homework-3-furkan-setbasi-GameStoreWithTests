using AutoMapper;
using DeveloperStore.Application.Common.Interfaces;
using GameStore.Application.Common.Exceptions;
using GameStore.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameStore.Application.Developers.Queries.GetDeveloperById
{
  public class GetDeveloperByIdQueryHandler : IRequestHandler<GetDeveloperByIdQuery, GetDeveloperByIdQueryResponse>
  {
    private readonly IDeveloperRepository _developerRepository;
    private readonly IMapper _mapper;

    public GetDeveloperByIdQueryHandler(IDeveloperRepository developerRepository, IMapper mapper)
    {
      _developerRepository = developerRepository;
      _mapper = mapper;
    }

    public async Task<GetDeveloperByIdQueryResponse> Handle(GetDeveloperByIdQuery request, CancellationToken cancellationToken)
    {
      Developer developer = await _developerRepository.GetByIdAsync(request.Id, cancellationToken);

      if(developer is null)
      {
        throw new NotFoundException("Developer was not found");
      }

      return _mapper.Map<GetDeveloperByIdQueryResponse>(developer);
    }
  }
}
