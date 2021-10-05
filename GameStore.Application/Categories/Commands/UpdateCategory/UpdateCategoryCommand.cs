using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Application.Categories.Commands.UpdateCategory
{
  public class UpdateCategoryCommand : IRequest<Unit>
  {
    public Guid Id { get; set; }
    public string Name { get; set; }

  }
}
