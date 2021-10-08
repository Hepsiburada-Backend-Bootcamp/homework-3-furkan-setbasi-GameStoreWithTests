using GameStore.Application.Categories.Commands.CreateCategory;
using GameStore.Application.Categories.Queries.GetCategories;
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
    private readonly string BASE_API_URL = "api/v1/categories";
    public CategortControllerTests(CustomWebApiFactory factory)
    {
      _factory = factory;
      _client = _factory.CreateClient();
    }

    [Fact]
    public async Task Get_ShouldReturnCategoryListWithOkResponse()
    {
      var response = await _client.GetAsync(BASE_API_URL);
      Assert.True(response.IsSuccessStatusCode);
    }

    [Fact]
    public async Task GetById_WhenNonExistingCategoryIdIsGiven_ShouldReturnNotFoundResponse()
    {
      var response = await _client.GetAsync(BASE_API_URL + Guid.NewGuid().ToString());
      Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Post_WhenValidInputsAreGiven_ShouldCreateGivenCategoryAndReturnCreatedCategoryIdWithCreatedResponse()
    {
      CreateCategoryCommand command = new() { Name = "New Category" };

      var commandJson = JsonSerializer.Serialize(command);

      var content = new StringContent(commandJson, Encoding.UTF8, "application/json");

      var createResponse = await _client.PostAsync(BASE_API_URL, content);
      var createResponseJsonString = await createResponse.Content.ReadAsStringAsync();

      var readResponse = await _client.GetAsync(BASE_API_URL);
      var readResponseJsonString = await readResponse.Content.ReadAsStringAsync();

      var categories = JsonSerializer.Deserialize<List<GetCategoriesQueryResponse>>(readResponseJsonString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

      var createdResponseJsonElement = JsonSerializer.Deserialize<JsonElement>(createResponseJsonString);

      var createdCategoryId = createdResponseJsonElement.GetProperty("createdCategoryId").GetGuid();

      Assert.Equal(HttpStatusCode.Created, createResponse.StatusCode);
      Assert.IsType<Guid>(createdCategoryId);
      Assert.Equal(categories[0].Id, createdCategoryId);
    }

    [Fact]
    public async Task Post_WhenInvalidInputsAreGiven_ShouldReturnBadRequestResponse()
    {
      CreateCategoryCommand command = new() { Name = "" };

      var commandJson = JsonSerializer.Serialize(command);

      var content = new StringContent(commandJson, Encoding.UTF8, "application/json");

      var createResponse = await _client.PostAsync(BASE_API_URL, content);

      Assert.Equal(HttpStatusCode.BadRequest, createResponse.StatusCode);
    }
  }
}
