using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Application.Games.Commands.AddCategory
{
  public class AddCategoryToGameCommand : IRequest
  {
    public Guid Id { get; set; }
    public string Name { get; set; }
  }
}
