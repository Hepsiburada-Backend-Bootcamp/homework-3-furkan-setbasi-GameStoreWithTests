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

namespace GameStore.Application.Categories.Queries.GetCategories
{
  public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, List<GetCategoriesQueryResponse>>
  {
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public GetCategoriesQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
    {
      _categoryRepository = categoryRepository;
      _mapper = mapper;
    }

    public async Task<List<GetCategoriesQueryResponse>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
      var categories = await _categoryRepository.GetAllAsync(cancellationToken);

      List<GetCategoriesQueryResponse> categoriesResponse = _mapper.Map<List<GetCategoriesQueryResponse>>(categories);

      return categoriesResponse;
    }
  }
}
