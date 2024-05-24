using System.Text.Json;
using FluentAssertions;
using Moq;

namespace Nutrition.APIs.Tests;

public class TestMealEndpoints
{
    [Fact]
    public async Task ListMeals_OnSucess_ReturnsStatusCode200()
    {
        // Arrange
        var mockMealService = new Mock<IMealService>();
        mockMealService
            .Setup(x => x.GetAll(It.IsAny<MealListRequest>()))
            .ReturnsAsync(MealFixture.GetTestMeals());
        var mockHttpContext = HttpContextHelper.CreateMockHttpContext();

        // Act
        var sut = await MealEndpoints.ListMeals(new MealListRequest(), mockMealService.Object);
        await sut.ExecuteAsync(mockHttpContext);

        // Assert
        mockHttpContext.Response.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task ListMeals_OnSucess_InvokesMealServiceExactlyOnce()
    {
        // Arrange
        var mockMealService = new Mock<IMealService>();
        mockMealService
            .Setup(x => x.GetAll(It.IsAny<MealListRequest>()))
            .ReturnsAsync(MealFixture.GetTestMeals());
        var mockHttpContext = HttpContextHelper.CreateMockHttpContext();

        // Act
        var sut = await MealEndpoints.ListMeals(new MealListRequest(), mockMealService.Object);
        await sut.ExecuteAsync(mockHttpContext);

        // Assert
        mockMealService.Verify(x => x.GetAll(It.IsAny<MealListRequest>()), Times.Once);
    }

    [Fact]
    public async Task ListMeals_OnSucess_ReturnsListOfFoods()
    {
        // Arrange
        var mockMealService = new Mock<IMealService>();
        mockMealService
            .Setup(x => x.GetAll(It.IsAny<MealListRequest>()))
            .ReturnsAsync(MealFixture.GetTestMeals());
        var mockHttpContext = HttpContextHelper.CreateMockHttpContext();

        // Act
        var sut = await MealEndpoints.ListMeals(new MealListRequest(), mockMealService.Object);
        await sut.ExecuteAsync(mockHttpContext);
        mockHttpContext.Response.Body.Position = 0;
        var jsonOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);
        var result = await JsonSerializer.DeserializeAsync<List<Meal>>(mockHttpContext.Response.Body, jsonOptions);

        // Assert
        result.Should().BeOfType<List<Meal>>();
    }
}
