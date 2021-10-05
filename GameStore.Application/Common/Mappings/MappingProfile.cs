using AutoMapper;
using GameStore.Application.Categories.Commands.CreateCategory;
using GameStore.Application.Categories.Queries.GetCategories;
using GameStore.Application.Categories.Queries.GetCategoryById;
using GameStore.Application.Developers.Commands.CreateDeveloper;
using GameStore.Application.Developers.Queries.GetDeveloperById;
using GameStore.Application.Developers.Queries.GetDevelopers;
using GameStore.Application.DTOs;
using GameStore.Application.Games.Commands.CreateGame;
using GameStore.Application.Games.Queries;
using GameStore.Application.Games.Queries.GetGameById;
using GameStore.Application.Games.Queries.GetGames;
using GameStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Application.Common.Mappings
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<CreateCategoryCommand, Category>();
      CreateMap<CreateGameCommand, Game>();
      CreateMap<CreateDeveloperCommand, Developer>();

      CreateMap<Category, GetCategoryByIdQueryResponse>();
      CreateMap<Category, GetCategoriesQueryResponse>();

      CreateMap<Game, GetGamesQueryResponse>();
      CreateMap<Game, GetGameByIdQueryResponse>();

      CreateMap<Developer, GetDevelopersQueryResponse>();
      CreateMap<Developer, GetDeveloperByIdQueryResponse>();

      CreateMap<Category, CategoryDto>();
      CreateMap<Developer, DeveloperDto>();
      CreateMap<Game, GameDto>();
    }
  }
}
