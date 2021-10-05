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

namespace GameStore.Application.Categories.Commands.DeleteCategory
{
  public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand>
  {
    private readonly ICategoryRepository _categoryRepository;
    public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository)
    {
      _categoryRepository = categoryRepository;
    }
    public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
      Category category = await _categoryRepository.GetByIdAsync(request.Id, cancellationToken);

      if(category is null)
      {
        throw new NotFoundException("Category was not found");
      }

      await _categoryRepository.DeleteAsync(category, cancellationToken);

      return Unit.Value;
    }
  }
}
