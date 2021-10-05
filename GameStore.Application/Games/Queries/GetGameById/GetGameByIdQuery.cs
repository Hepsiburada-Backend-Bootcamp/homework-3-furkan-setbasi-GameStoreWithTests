using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Application.Games.Queries.GetGameById
{
  public class GetGameByIdQuery : IRequest<GetGameByIdQueryResponse>
  {
    public Guid Id { get; set; }
  }
}
