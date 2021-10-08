using GameStore.API;
using GameStore.Infrastructure.Data.Contexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.IntegrationTests
{
  public class CustomWebApiFactory : WebApplicationFactory<Startup>
  {
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
      builder.ConfigureServices(services => {
        var descriptor = services.SingleOrDefault(
          d => d.ServiceType ==
          typeof(DbContextOptions<GameStoreDbContext>));

        services.Remove(descriptor);

        services.AddDbContext<GameStoreDbContext>(options => {
          options.UseInMemoryDatabase("GameStoreTestDb");
        });

        var sp = services.BuildServiceProvider();
        using(var scope = sp.CreateScope())
        {
          var scopedServices = scope.ServiceProvider;
          var db = scopedServices.GetRequiredService<GameStoreDbContext>();
          db.Database.EnsureCreated();
        }
        
      });
    }
  }
}
