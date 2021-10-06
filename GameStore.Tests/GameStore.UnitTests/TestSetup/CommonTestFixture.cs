using AutoMapper;
using GameStore.Application.Common.Mappings;
using GameStore.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.UnitTests.TestSetup
{
  public class CommonTestFixture
  {
    public GameStoreDbContext Context { get; set; }
    public IMapper Mapper { get; set; }
    public IConfiguration Configuration { get; set; }

    public CommonTestFixture()
    {
      var options = new DbContextOptionsBuilder<GameStoreDbContext>().UseInMemoryDatabase(databaseName: "GameStoreTestDB").Options;
      Context = new GameStoreDbContext(options);
      Context.Database.EnsureCreated();

      Mapper = new MapperConfiguration(config => { config.AddProfile<MappingProfile>(); }).CreateMapper();

      Configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
    }
  }
}
