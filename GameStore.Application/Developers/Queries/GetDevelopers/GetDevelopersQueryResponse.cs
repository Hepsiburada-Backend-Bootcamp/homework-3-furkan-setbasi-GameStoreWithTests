using GameStore.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Application.Developers.Queries.GetDevelopers
{
  public class GetDevelopersQueryResponse
  {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Country { get; set; }
    public ICollection<GameDto> Games { get; set; }
  }
}
