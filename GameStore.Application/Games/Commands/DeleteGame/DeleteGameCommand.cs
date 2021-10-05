using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Application.Games.Commands.DeleteGame
{
  public class DeleteGameCommand : IRequest
  {
    public Guid Id { get; set; }
  }
}
