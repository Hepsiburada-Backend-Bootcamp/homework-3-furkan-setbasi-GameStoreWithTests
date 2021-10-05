using AutoMapper;
using GameStore.Application.Common.Interfaces;
using GameStore.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameStore.Application.Categories.Commands.CreateCategory
{
  public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Guid>
  {
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CreateCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper)
    {
      _categoryRepository = categoryRepository;
      _mapper = mapper;
    }

    public async Task<Guid> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
      Category newCategory = _mapper.Map<Category>(request);

      Guid createdCategoryId = await _categoryRepository.CreateAsync(newCategory, cancellationToken);
      return createdCategoryId;
    }
  }
}
