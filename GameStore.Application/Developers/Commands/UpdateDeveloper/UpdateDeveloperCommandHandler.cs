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

namespace GameStore.Application.Developers.Commands.UpdateDeveloper
{
  public class UpdateDeveloperCommandHandler : IRequestHandler<UpdateDeveloperCommand, Unit>
  {
    private readonly IDeveloperRepository _developerRepository;

    public UpdateDeveloperCommandHandler(IDeveloperRepository developerRepository)
    {
      _developerRepository = developerRepository;
    }

    public async Task<Unit> Handle(UpdateDeveloperCommand request, CancellationToken cancellationToken)
    {
      Developer developerToUpdate = await _developerRepository.GetByIdAsync(request.Id, cancellationToken);

      if(developerToUpdate is null)
      {
        throw new NotFoundException("Developer was not found.");
      }

      await _developerRepository.UpdateAsync(developerToUpdate, request, cancellationToken);

      return Unit.Value;
    }
  }
}
