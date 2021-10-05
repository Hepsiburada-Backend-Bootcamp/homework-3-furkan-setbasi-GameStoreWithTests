using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Application.Developers.Commands.DeleteDeveloper
{
  public class DeleteDeveloperCommand : IRequest
  {
    public Guid Id { get; set; }
  }
}
