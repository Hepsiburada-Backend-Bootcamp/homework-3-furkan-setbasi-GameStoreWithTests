using AutoMapper;
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

namespace GameStore.Application.Categories.Commands.UpdateCategory
{
  public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Unit>
  {
    private readonly ICategoryRepository _categoryRepository;

    public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository)
    {
      _categoryRepository = categoryRepository;
    }

    public async Task<Unit> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
      Category categoryToUpdate = await _categoryRepository.GetByIdAsync(request.Id, cancellationToken);

      if(categoryToUpdate is null)
      {
        throw new NotFoundException("Category was not found");
      }

      await _categoryRepository.UpdateAsync(categoryToUpdate, request, cancellationToken);

      return Unit.Value;
    }
  }
}
