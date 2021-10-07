using GameStore.Application.Common.Interfaces;
using GameStore.Application.Games.Commands.UpdateGame;
using GameStore.Domain.Entities;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameStore.UnitTests.TestSetup.Mocks
{
  public class MockGameRepository
  {
    public static Mock<IGameRepository> GetMockGameRepository()
    {
      List<Game> games = new List<Game>()
      {
        CreateGame(),CreateGame(),CreateGame()
      };

      var mockGameRepository = new Mock<IGameRepository>();

      mockGameRepository.Setup(repo => repo.GetAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync(games);

      mockGameRepository
        .Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
        .ReturnsAsync(

        (Guid gameId, CancellationToken cancellationToken) => {
          return games.Find(game => game.Id == gameId);
        }

        );

      mockGameRepository
        .Setup(repo => repo.AddCategoryAsync(It.IsAny<Game>(), It.IsAny<Category>(), It.IsAny<CancellationToken>()))
        .ReturnsAsync(
          (Game game, Category category, CancellationToken cancellationToken) => {

            var foundGame = games.Find(g => g.Id == game.Id);
            foundGame.Categories.Add(category);

            return Unit.Value;
          }
        );

      mockGameRepository
        .Setup(repo => repo.CreateAsync(It.IsAny<Game>(), It.IsAny<CancellationToken>()))
        .ReturnsAsync(

        (Game game, CancellationToken cancellationToken) => {
          game.Id = Guid.NewGuid();
          games.Add(game);
          return game.Id;
        }

        );

      mockGameRepository
        .Setup(repo => repo.UpdateAsync(It.IsAny<Game>(), It.IsAny<UpdateGameCommand>(), It.IsAny<CancellationToken>()))
        .ReturnsAsync(

        (Game game, UpdateGameCommand request, CancellationToken cancellationToken) => {
          Game gameToUpdate = games.Find(c => c.Id == request.Id);

          game.Name = string.IsNullOrWhiteSpace(request.Name) ? game.Name : request.Name;
          game.Price = request.Price == default ? game.Price : request.Price;

          return Unit.Value;
        }

        );

      mockGameRepository
        .Setup(repo => repo.DeleteAsync(It.IsAny<Game>(), It.IsAny<CancellationToken>()))
        .ReturnsAsync(

        (Game game, CancellationToken cancellationToken) => {
          games.Remove(game);

          return Unit.Value;
        }

        );

      return mockGameRepository;
    }

    private static Game CreateGame()
    {
      return new()
      {
        Id = Guid.NewGuid(),
        Name = Guid.NewGuid().ToString(),
        Price = 99,
        Categories = new List<Category>(),
        Developer = new Developer(),
        DeveloperId = new Guid()
      };
    }
  }
}
