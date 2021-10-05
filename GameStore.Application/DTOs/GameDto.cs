using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Application.DTOs
{
  public class GameDto
  {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Price { get; set; }
    public List<CategoryDto> Categories { get; set; }
  }
}
