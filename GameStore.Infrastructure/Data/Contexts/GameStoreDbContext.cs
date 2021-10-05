using GameStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Infrastructure.Data.Contexts
{
  public class GameStoreDbContext : DbContext
  {
    public GameStoreDbContext(DbContextOptions<GameStoreDbContext> options) : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Developer> Developers { get; set; }
    public DbSet<Game> Games { get; set; }

  }
}
