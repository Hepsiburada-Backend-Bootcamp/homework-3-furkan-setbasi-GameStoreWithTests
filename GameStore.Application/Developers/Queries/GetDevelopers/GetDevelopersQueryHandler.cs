using AutoMapper;
using DeveloperStore.Application.Common.Interfaces;
using GameStore.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameStore.Application.Developers.Queries.GetDevelopers
{
  public class GetDevelopersQueryHandler : IRequestHandler<GetDevelopersQuery, List<GetDevelopersQueryResponse>>
  {
    private readonly IDeveloperRepository _developerRepository;
    private readonly IMapper _mapper;

    public GetDevelopersQueryHandler(IDeveloperRepository developerRepository, IMapper mapper)
    {
      _developerRepository = developerRepository;
      _mapper = mapper;
    }

    public async Task<List<GetDevelopersQueryResponse>> Handle(GetDevelopersQuery request, CancellationToken cancellationToken)
    {
      List<Developer> developers = await _developerRepository.GetAllAsync(cancellationToken);

      List<GetDevelopersQueryResponse> developersResponse = _mapper.Map<List<GetDevelopersQueryResponse>>(developers);

      return developersResponse;
    }
  }
}
