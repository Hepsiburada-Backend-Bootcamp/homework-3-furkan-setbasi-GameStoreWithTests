using GameStore.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameStore.Application.Games.Queries.GetGameById
{
  public class GetGameByIdQueryResponse
  {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Price { get; set; }
    public DeveloperDto Developer { get; set; }
    public List<CategoryDto> Categories { get; set; }
  }
}
