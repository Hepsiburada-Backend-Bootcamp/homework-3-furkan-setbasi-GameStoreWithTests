using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Application.Developers.Commands.CreateDeveloper
{
  public class CreateDeveloperCommand : IRequest<Guid>
  {
    public string Name { get; set; }
    public string Country { get; set; }
  }
}
