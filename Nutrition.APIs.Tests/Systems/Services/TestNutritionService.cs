using FluentAssertions;
using Moq;
using Moq.EntityFrameworkCore;

namespace Nutrition.APIs.Tests;

public class TestNutritionService
{
    [Fact]
    public async Task GetAll_WhenCalled_ReturnsListOfFoods()
    {
        // Arrange
        var mockNutritionContext = new Mock<NutritionContext>();
        mockNutritionContext
            .Setup(c => c.Nutritions)
            .ReturnsDbSet(NutritionFixture.GetTestNutritions());

        var sut = new NutritionService(mockNutritionContext.Object);

        // Act
        var nutritions = await sut.GetAll();

        // Assert
        nutritions.Should().HaveCount(NutritionFixture.GetTestNutritions().Count);
    }
}
