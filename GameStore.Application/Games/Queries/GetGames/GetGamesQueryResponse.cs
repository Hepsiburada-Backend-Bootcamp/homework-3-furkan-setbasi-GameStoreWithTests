using GameStore.Application.DTOs;
using GameStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Application.Games.Queries.GetGames
{
  public class GetGamesQueryResponse
  {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Price { get; set; }
    public DeveloperDto Developer { get; set; }
    public List<CategoryDto> Categories { get; set; }
  }
}
