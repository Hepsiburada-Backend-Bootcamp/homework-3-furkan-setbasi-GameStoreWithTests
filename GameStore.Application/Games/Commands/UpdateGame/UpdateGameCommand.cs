using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Application.Games.Commands.UpdateGame
{
  public class UpdateGameCommand : IRequest
  {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Price { get; set; }
    public Guid? DeveloperId { get; set; }
  }
}
