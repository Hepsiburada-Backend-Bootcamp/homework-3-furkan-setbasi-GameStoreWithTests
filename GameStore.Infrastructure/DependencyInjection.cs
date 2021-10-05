using DeveloperStore.Application.Common.Interfaces;
using GameStore.Application.Common.Interfaces;
using GameStore.Infrastructure.Data.Contexts;
using GameStore.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Infrastructure
{
  public static class DependencyInjection
  {
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
      services.AddDbContext<GameStoreDbContext>(options => options.UseInMemoryDatabase("GameStoreDb"));
      services.AddScoped<ICategoryRepository, CategoryRepository>();
      services.AddScoped<IGameRepository, GameRepository>();
      services.AddScoped<IDeveloperRepository, DeveloperRepository>();



      return services;
    }
  }
}
