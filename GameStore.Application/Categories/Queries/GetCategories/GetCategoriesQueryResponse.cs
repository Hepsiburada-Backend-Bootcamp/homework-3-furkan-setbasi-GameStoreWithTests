using GameStore.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Application.Categories.Queries.GetCategories
{
  public class GetCategoriesQueryResponse
  {
    public Guid Id { get; set; }
    public string Name { get; set; }

    public ICollection<GameDto> Games { get; set; }
  }
}
