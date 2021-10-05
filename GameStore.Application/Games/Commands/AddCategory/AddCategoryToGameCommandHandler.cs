using GameStore.Application.Common.Exceptions;
using GameStore.Application.Common.Interfaces;
using GameStore.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameStore.Application.Games.Commands.AddCategory
{
  public class AddCategoryToGameCommandHandler : IRequestHandler<AddCategoryToGameCommand>
  {
    private readonly ICategoryRepository _categoryRepository;
    private readonly IGameRepository _gameRepository;

    public AddCategoryToGameCommandHandler(ICategoryRepository categoryRepository, IGameRepository gameRepository)
    {
      _categoryRepository = categoryRepository;
      _gameRepository = gameRepository;
    }

    public async Task<Unit> Handle(AddCategoryToGameCommand request, CancellationToken cancellationToken)
    {
      Game game = await _gameRepository.GetByIdAsync(request.Id, cancellationToken);

      if(game is null)
      {
        throw new NotFoundException("Game was not found");
      }

      Category category = await _categoryRepository.GetByNameAsync(request.Name, cancellationToken);

      if(category is not null)
      {
        await _gameRepository.AddCategoryAsync(game, category, cancellationToken);
        return Unit.Value;
      }

      Category newCategory = new Category()
      {
        Name = request.Name
      };

      Guid createdCategoryId = await _categoryRepository.CreateAsync(newCategory, cancellationToken);

      Category createdCategory = await _categoryRepository.GetByIdAsync(createdCategoryId, cancellationToken);

      await _gameRepository.AddCategoryAsync(game, createdCategory, cancellationToken);

      return Unit.Value;

    }
  }
}
