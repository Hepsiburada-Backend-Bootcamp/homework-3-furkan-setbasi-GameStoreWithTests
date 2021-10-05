using GameStore.Application.Developers.Commands.UpdateDeveloper;
using GameStore.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DeveloperStore.Application.Common.Interfaces
{
  public interface IDeveloperRepository
  {
    Task<List<Developer>> GetAllAsync(CancellationToken cancellationToken);
    Task<Developer> GetByIdAsync(Guid id, CancellationToken cancellation);
    Task<Guid> CreateAsync(Developer game, CancellationToken cancellationToken);
    Task<Unit> UpdateAsync(Developer game, UpdateDeveloperCommand request, CancellationToken cancellation);
    Task<Unit> DeleteAsync(Developer game, CancellationToken cancellationToken);
  }
}
