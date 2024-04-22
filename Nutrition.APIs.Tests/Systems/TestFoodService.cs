using FluentAssertions;
using MockQueryable.Moq;
using Moq;

namespace Nutrition.APIs.Tests;

public class TestFoodService
{
    [Fact]
    public async Task GetFoods_WhenCalled_ReturnsListOfFoods()
    {
        // Arrange
        var mock = FoodFixture.GetTestFoods().AsQueryable().BuildMockDbSet();
        var mockNutritionContext = new Mock<INutritionContext>();
        mockNutritionContext
            .Setup(c => c.Foods)
            .Returns(mock.Object);

        var sut = new FoodService(mockNutritionContext.Object);

        // Act
        var foods = await sut.GetFoods();

        // Assert
        foods.Should().HaveCount(FoodFixture.GetTestFoods().Count);
    }
}
