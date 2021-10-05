using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.Entities
{
  public class Game
  {
    public Guid Id { get; set; }
    public string Name { get; set; }

    public int Price { get; set; }
    public ICollection<Category> Categories { get; set; }

    [ForeignKey("Developer")]
    public Guid? DeveloperId { get; set; }

    public Developer Developer { get; set; }
  }
}
