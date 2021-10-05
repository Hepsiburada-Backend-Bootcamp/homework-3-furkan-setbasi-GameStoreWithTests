using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Application.Games.Commands.CreateGame
{
  public class CreateGameCommand : IRequest<Guid>
  {
    public string Name { get; set; }
    public int Price { get; set; }
    public Guid? DeveloperId { get; set; }
  }
}
