using GameStore.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Application.Categories.Queries.GetCategories
{
  public class GetCategoriesQuery : IRequest<List<GetCategoriesQueryResponse>>
  {
  }
}
