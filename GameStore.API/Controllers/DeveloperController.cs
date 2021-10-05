using GameStore.Application.Developers.Commands.CreateDeveloper;
using GameStore.Application.Developers.Commands.DeleteDeveloper;
using GameStore.Application.Developers.Commands.UpdateDeveloper;
using GameStore.Application.Developers.Queries.GetDeveloperById;
using GameStore.Application.Developers.Queries.GetDevelopers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.API.Controllers
{
  [Route("api/v1/[controller]s")]
  [ApiController]
  public class DeveloperController : ApiControllerBase
  {
    [HttpGet]
    public async Task<ActionResult<List<GetDevelopersQueryResponse>>> Get()
    {
      return await Mediator.Send(new GetDevelopersQuery());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetDeveloperByIdQueryResponse>> GetById(Guid id)
    {
      GetDeveloperByIdQuery query = new GetDeveloperByIdQuery()
      {
        Id = id
      };

      return await Mediator.Send(query);
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateDeveloperCommand command)
    {
      Guid createdDeveloperId = await Mediator.Send(command);
      return Created(new Uri($"{Request.Path}/{createdDeveloperId}", UriKind.Relative), new { createdDeveloperId });
    }

    [HttpPut]
    public async Task<ActionResult<Unit>> Put(Guid id, [FromBody] UpdateDeveloperCommand command)
    {
      command.Id = id;

      return await Mediator.Send(command);
    }

    [HttpDelete]
    public async Task<ActionResult<Unit>> Delete(Guid id)
    {
      DeleteDeveloperCommand command = new DeleteDeveloperCommand()
      {
        Id = id
      };

      await Mediator.Send(command);

      return NoContent();
    }
  }
}
