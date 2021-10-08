using GameStore.Application.Categories.Commands.CreateCategory;
using GameStore.Application.Categories.Commands.DeleteCategory;
using GameStore.Application.Categories.Commands.UpdateCategory;
using GameStore.Application.Categories.Queries.GetCategories;
using GameStore.Application.Categories.Queries.GetCategoryById;
using GameStore.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.API.Controllers
{
  [Route("api/v1/categories")]
  [ApiController]
  public class CategoryController : ApiControllerBase
  {
    [HttpGet]
    public async Task<ActionResult<List<GetCategoriesQueryResponse>>> Get()
    {
      return await Mediator.Send(new GetCategoriesQuery());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetCategoryByIdQueryResponse>> GetById(Guid id)
    {
      GetCategoryByIdQuery query = new GetCategoryByIdQuery()
      {
        Id = id
      };

      var result = await Mediator.Send(query);

      return result;
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateCategoryCommand command)
    {
      Guid createdCategoryId = await Mediator.Send(command);
      return Created(new Uri($"{Request.Path}/{createdCategoryId}", UriKind.Relative), new { createdCategoryId });
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Unit>> Put(Guid id, [FromBody] UpdateCategoryCommand command)
    {
      command.Id = id;

      return await Mediator.Send(command);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Unit>> Delete(Guid id)
    {
      DeleteCategoryCommand command = new DeleteCategoryCommand()
      {
        Id = id
      };

      await Mediator.Send(command);

      return NoContent();
    }
  }
}
