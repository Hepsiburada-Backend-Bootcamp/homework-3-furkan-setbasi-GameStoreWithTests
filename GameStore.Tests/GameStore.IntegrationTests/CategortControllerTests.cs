using GameStore.Application.Categories.Commands.CreateCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace GameStore.IntegrationTests
{
  public class CategortControllerTests : IClassFixture<CustomWebApiFactory>
  {
    private readonly HttpClient _client;
    private readonly CustomWebApiFactory _factory;
    public CategortControllerTests(CustomWebApiFactory factory)
    {
      _factory = factory;
      _client = factory.CreateClient();
    }

    [Fact]
    public async Task Get_ShouldReturnCategoryListWithOkResponse()
    {
      var response = await _client.GetAsync("api/v1/categories");
      Assert.True(response.IsSuccessStatusCode);
    }

    [Fact]
    public async Task GetById_WhenNonExistingIdIsGiven_ShouldReturnNotFoundResponse()
    {
      var response = await _client.GetAsync("api/v1/categories/" + Guid.NewGuid().ToString());
      Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Post_ShouldCreateGivenCategoryAndReturnCreatedResponse()
    {
      CreateCategoryCommand command = new() { Name = "New Category" };

      var commandJson = JsonSerializer.Serialize(command);

      var content = new StringContent(commandJson, Encoding.UTF8, "application/json");

      var createResponse = await _client.PostAsync("api/v1/categories", content);

      Assert.Equal(HttpStatusCode.Created, createResponse.StatusCode);
    }

    [Fact]
    public async Task Post_WhenInvalidDataIsGiven_ShouldReturnBadRequest()
    {
      CreateCategoryCommand command = new() { Name = "" };

      var commandJson = JsonSerializer.Serialize(command);

      var content = new StringContent(commandJson, Encoding.UTF8, "application/json");

      var createResponse = await _client.PostAsync("api/v1/categories", content);

      Assert.Equal(HttpStatusCode.BadRequest, createResponse.StatusCode);
    }
  }
}
