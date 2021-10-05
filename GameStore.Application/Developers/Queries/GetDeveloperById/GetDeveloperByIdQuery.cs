using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Application.Developers.Queries.GetDeveloperById
{
  public class GetDeveloperByIdQuery : IRequest<GetDeveloperByIdQueryResponse>
  {
    public Guid Id { get; set; }
  }
}
