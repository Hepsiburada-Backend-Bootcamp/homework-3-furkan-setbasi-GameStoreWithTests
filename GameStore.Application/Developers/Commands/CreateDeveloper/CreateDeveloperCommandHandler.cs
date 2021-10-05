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

namespace GameStore.Application.Developers.Commands.CreateDeveloper
{
  public class CreateDeveloperCommandHandler : IRequestHandler<CreateDeveloperCommand, Guid>
  {
    private readonly IDeveloperRepository _developerRepository;
    private readonly IMapper _mapper;

    public CreateDeveloperCommandHandler(IDeveloperRepository developerRepository, IMapper mapper)
    {
      _developerRepository = developerRepository;
      _mapper = mapper;
    }

    public async Task<Guid> Handle(CreateDeveloperCommand request, CancellationToken cancellationToken)
    {
      Developer developer = _mapper.Map<Developer>(request);

      Guid createdDeveloperId = await _developerRepository.CreateAsync(developer, cancellationToken);
      return createdDeveloperId;
    }
  }
}
