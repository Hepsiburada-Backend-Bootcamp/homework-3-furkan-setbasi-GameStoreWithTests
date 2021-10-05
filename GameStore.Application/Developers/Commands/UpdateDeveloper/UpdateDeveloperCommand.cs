using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Application.Developers.Commands.UpdateDeveloper
{
  public class UpdateDeveloperCommand : IRequest<Unit>
  {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Country { get; set; }
  }
}
