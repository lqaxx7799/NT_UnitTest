using System.Text.Json;
using FluentAssertions;
using Moq;

namespace Nutrition.APIs.Tests;

public class TestFoodEndpoints
{
    [Fact]
    public async Task ListFoods_OnSucess_ReturnsStatusCode200()
    {
        // Arrange
        var mockFoodService = new Mock<IFoodService>();
        mockFoodService
            .Setup(x => x.GetFoods())
            .ReturnsAsync(FoodFixture.GetTestFoods());
        var mockHttpContext = HttpContextHelper.CreateMockHttpContext();

        // Act
        var sut = await FoodEndpoints.ListFoods(mockFoodService.Object);
        await sut.ExecuteAsync(mockHttpContext);

        // Assert
        mockHttpContext.Response.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task ListFoods_OnSucess_InvokesFoodServiceExactlyOnce()
    {
        // Arrange
        var mockFoodService = new Mock<IFoodService>();
        mockFoodService
            .Setup(x => x.GetFoods())
            .ReturnsAsync(FoodFixture.GetTestFoods());
        var mockHttpContext = HttpContextHelper.CreateMockHttpContext();

        // Act
        var sut = await FoodEndpoints.ListFoods(mockFoodService.Object);
        await sut.ExecuteAsync(mockHttpContext);

        // Assert
        mockFoodService.Verify(x => x.GetFoods(), Times.Once);
    }

    [Fact]
    public async Task ListFoods_OnSucess_ReturnsListOfFoods()
    {
        // Arrange
        var mockFoodService = new Mock<IFoodService>();
        mockFoodService
            .Setup(x => x.GetFoods())
            .ReturnsAsync(FoodFixture.GetTestFoods());
        var mockHttpContext = HttpContextHelper.CreateMockHttpContext();

        // Act
        var sut = await FoodEndpoints.ListFoods(mockFoodService.Object);
        await sut.ExecuteAsync(mockHttpContext);
        mockHttpContext.Response.Body.Position = 0;
        var jsonOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);
        var result = await JsonSerializer.DeserializeAsync<List<Food>>(mockHttpContext.Response.Body, jsonOptions);

        // Assert
        result.Should().BeOfType<List<Food>>();
    }

    [Theory]
    [InlineData("62FA647C-AD54-4BCC-A860-E5A2664B019D")]
    [InlineData("7B1F3580-63EB-4686-8588-8BAD339D48A5")]
    public async Task GetFood_OnSucess_ReturnsStatusCode200(string id)
    {
        // Arrange
        var foodId = Guid.Parse(id);
        var mockFoodService = new Mock<IFoodService>();
        mockFoodService
            .Setup(x => x.GetFood(foodId))
            .ReturnsAsync(new Food());
        var mockHttpContext = HttpContextHelper.CreateMockHttpContext();

        // Act
        var sut = await FoodEndpoints.GetFood(foodId, mockFoodService.Object);
        await sut.ExecuteAsync(mockHttpContext);

        // Assert
        mockHttpContext.Response.StatusCode.Should().Be(200);
    }

    [Theory]
    [InlineData("62FA647C-AD54-4BCC-A860-E5A2664B019D")]
    [InlineData("7B1F3580-63EB-4686-8588-8BAD339D48A5")]
    public async Task GetFood_OnSucess_InvokesFoodService(string id)
    {
        // Arrange
        var foodId = Guid.Parse(id);
        var mockFoodService = new Mock<IFoodService>();
        mockFoodService
            .Setup(x => x.GetFood(foodId))
            .ReturnsAsync(new Food());
        var mockHttpContext = HttpContextHelper.CreateMockHttpContext();

        // Act
        var sut = await FoodEndpoints.GetFood(foodId, mockFoodService.Object);
        await sut.ExecuteAsync(mockHttpContext);

        // Assert
        mockFoodService.Verify(x => x.GetFood(foodId), Times.Once);
    }

    [Theory]
    [InlineData("62FA647C-AD54-4BCC-A860-E5A2664B019D")]
    [InlineData("7B1F3580-63EB-4686-8588-8BAD339D48A5")]
    public async Task GetFood_OnNotFound_ReturnsStatusCode404(string id)
    {
        // Arrange
        var foodId = Guid.Parse(id);
        var mockFoodService = new Mock<IFoodService>();
        mockFoodService
            .Setup(x => x.GetFood(foodId))
            .ReturnsAsync((Food?)null);
        var mockHttpContext = HttpContextHelper.CreateMockHttpContext();

        // Act
        var sut = await FoodEndpoints.GetFood(foodId, mockFoodService.Object);
        await sut.ExecuteAsync(mockHttpContext);

        // Assert
        mockHttpContext.Response.StatusCode.Should().Be(404);
    }
}
