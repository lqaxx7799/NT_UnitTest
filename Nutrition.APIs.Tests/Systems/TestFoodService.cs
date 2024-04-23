using FluentAssertions;
using Moq;
using Moq.EntityFrameworkCore;

namespace Nutrition.APIs.Tests;

public class TestFoodService
{
    [Fact]
    public async Task GetFoods_WhenCalled_ReturnsListOfFoods()
    {
        // Arrange
        var mockNutritionContext = new Mock<NutritionContext>();
        mockNutritionContext
            .Setup(c => c.Foods)
            .ReturnsDbSet(FoodFixture.GetTestFoods());

        var sut = new FoodService(mockNutritionContext.Object);

        // Act
        var foods = await sut.GetFoods();

        // Assert
        foods.Should().HaveCount(FoodFixture.GetTestFoods().Count);
    }
}
