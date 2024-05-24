using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;

namespace Nutrition.APIs.Tests;

public class TestReportService
{
    [Fact]
    public async Task GetNutritionProfile_WhenCalled_InvokesNutritionContext()
    {
        // Arrange
        var mockDbContextOptions = new DbContextOptions<NutritionContext>();
        var mockNutritionContext = new Mock<NutritionContext>(mockDbContextOptions);
        mockNutritionContext
            .Setup(c => c.Meals)
            .ReturnsDbSet(MealFixture.GetTestMeals());
        mockNutritionContext
            .Setup(c => c.Nutritions)
            .ReturnsDbSet(NutritionFixture.GetTestNutritions());
        var sut = new ReportService(mockNutritionContext.Object);

        // Act
        var result = await sut.GetNutritionProfile(new ReportNutritionProfileRequest());

        // Assert
        mockNutritionContext.Verify(x => x.Meals, Times.Once);
    }
}
