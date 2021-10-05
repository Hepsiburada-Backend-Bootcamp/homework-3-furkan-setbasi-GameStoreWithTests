using GameStore.Application.Games.Commands.AddCategory;
using GameStore.Application.Games.Commands.CreateGame;
using GameStore.Application.Games.Commands.DeleteGame;
using GameStore.Application.Games.Commands.UpdateGame;
using GameStore.Application.Games.Queries.GetGameById;
using GameStore.Application.Games.Queries.GetGames;
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
  public class GameController : ApiControllerBase
  {

    [HttpGet]
    public async Task<ActionResult<List<GetGamesQueryResponse>>> Get()
    {
      return await Mediator.Send(new GetGamesQuery());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetGameByIdQueryResponse>> GetById(Guid id)
    {
      GetGameByIdQuery query = new GetGameByIdQuery()
      {
        Id = id
      };

      return await Mediator.Send(query);
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateGameCommand command)
    {
      Guid createdGameId = await Mediator.Send(command);
      return Created(new Uri($"{Request.Path}/{createdGameId}", UriKind.Relative), new { createdGameId });
    }

    [HttpPost("{id}/categories")]
    public async Task<IActionResult> CreateCategory(Guid id, AddCategoryToGameCommand command)
    {
      command.Id = id;
      await Mediator.Send(command);
      return NoContent();
    }

    [HttpPut]
    public async Task<ActionResult<Unit>> Put(Guid id, [FromBody] UpdateGameCommand command)
    {
      command.Id = id;

      return await Mediator.Send(command);
    }

    [HttpDelete]
    public async Task<ActionResult<Unit>> Delete(Guid id)
    {
      DeleteGameCommand command = new DeleteGameCommand()
      {
        Id = id
      };

      await Mediator.Send(command);

      return NoContent();
    }

  }
}
