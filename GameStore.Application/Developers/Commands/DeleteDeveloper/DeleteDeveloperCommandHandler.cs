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

namespace GameStore.Application.Developers.Commands.DeleteDeveloper
{
  public class DeleteDeveloperCommandHandler : IRequestHandler<DeleteDeveloperCommand>
  {
    private readonly IDeveloperRepository _developerRepository;

    public DeleteDeveloperCommandHandler(IDeveloperRepository developerRepository)
    {
      _developerRepository = developerRepository;
    }

    public async Task<Unit> Handle(DeleteDeveloperCommand request, CancellationToken cancellationToken)
    {
      Developer developer = await _developerRepository.GetByIdAsync(request.Id, cancellationToken);

      if(developer is null)
      {
        throw new NotFoundException("Developer was not found");
      }

      await _developerRepository.DeleteAsync(developer, cancellationToken);

      return Unit.Value;
    }
  }
}
