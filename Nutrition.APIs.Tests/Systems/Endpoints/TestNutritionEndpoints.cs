using System.Text.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;

namespace Nutrition.APIs.Tests;

public class TestNutritionEndpoints
{
    [Fact]
    public async Task ListNutritions_OnSuccess_ReturnsStatusCode200()
    {
        // Arrange
        var mockNutritionService = new Mock<INutritionService>();
        var mockHttpContext = HttpContextHelper.CreateMockHttpContext();

        // Act
        var sut = await NutritionEndpoints.ListNutritions(mockNutritionService.Object);
        await sut.ExecuteAsync(mockHttpContext);

        // Assert
        mockHttpContext.Response.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task ListNutritions_OnSuccess_InvokesNutritionService()
    {
        // Arrange
        var mockNutritionService = new Mock<INutritionService>();
        var mockHttpContext = HttpContextHelper.CreateMockHttpContext();

        // Act
        var sut = await NutritionEndpoints.ListNutritions(mockNutritionService.Object);
        await sut.ExecuteAsync(mockHttpContext);

        // Assert
        mockNutritionService.Verify(x => x.GetAll(), Times.Once);
    }

    [Fact]
    public async Task ListNutritions_OnSuccess_ReturnsListOfNutritions()
    {
        // Arrange
        var mockNutritionService = new Mock<INutritionService>();
        mockNutritionService
            .Setup(x => x.GetAll())
            .ReturnsAsync(NutritionFixture.GetTestNutritions());
        var mockHttpContext = HttpContextHelper.CreateMockHttpContext();

        // Act
        var sut = await NutritionEndpoints.ListNutritions(mockNutritionService.Object);
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
    public async Task GetOne_OnSucess_ReturnsStatusCode200(string id)
    {
        // Arrange
        var nutritionId = Guid.Parse(id);
        var mockNutritionService = new Mock<INutritionService>();
        mockNutritionService
            .Setup(x => x.GetOne(nutritionId))
            .ReturnsAsync(new Nutrition());
        var mockHttpContext = HttpContextHelper.CreateMockHttpContext();

        // Act
        var sut = await NutritionEndpoints.GetOne(nutritionId, mockNutritionService.Object);
        await sut.ExecuteAsync(mockHttpContext);

        // Assert
        mockHttpContext.Response.StatusCode.Should().Be(200);
    }

    [Theory]
    [InlineData("62FA647C-AD54-4BCC-A860-E5A2664B019D")]
    [InlineData("7B1F3580-63EB-4686-8588-8BAD339D48A5")]
    public async Task GetOne_OnSucess_InvokesFoodService(string id)
    {
        // Arrange
        var nutritionId = Guid.Parse(id);
        var mockNutritionService = new Mock<INutritionService>();
        mockNutritionService
            .Setup(x => x.GetOne(nutritionId))
            .ReturnsAsync(new Nutrition());
        var mockHttpContext = HttpContextHelper.CreateMockHttpContext();

        // Act
        var sut = await NutritionEndpoints.GetOne(nutritionId, mockNutritionService.Object);
        await sut.ExecuteAsync(mockHttpContext);

        // Assert
        mockNutritionService.Verify(x => x.GetOne(nutritionId), Times.Once);
    }

    [Theory]
    [InlineData("62FA647C-AD54-4BCC-A860-E5A2664B019D")]
    [InlineData("7B1F3580-63EB-4686-8588-8BAD339D48A5")]
    public async Task GetFood_OnNotFound_ReturnsStatusCode404(string id)
    {
        // Arrange
        var nutritionId = Guid.Parse(id);
        var mockNutritionService = new Mock<INutritionService>();
        mockNutritionService
            .Setup(x => x.GetOne(nutritionId))
            .ReturnsAsync((Nutrition?)null);
        var mockHttpContext = HttpContextHelper.CreateMockHttpContext();

        // Act
        var sut = await NutritionEndpoints.GetOne(nutritionId, mockNutritionService.Object);
        await sut.ExecuteAsync(mockHttpContext);

        // Assert
        mockHttpContext.Response.StatusCode.Should().Be(404);
    }

}
