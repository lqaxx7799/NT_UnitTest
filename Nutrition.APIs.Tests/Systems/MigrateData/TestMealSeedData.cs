using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;

namespace Nutrition.APIs.Tests;

public class TestMealSeedData
{
    [Fact]
    public async Task SeedData_WhenCalled_DataInserted()
    {
        // Arrange
        var dbContextOptions = new DbContextOptions<NutritionContext>();
        var mockNutritionContext = new Mock<NutritionContext>(dbContextOptions);
        var dbContextMealTypes = new List<MealType>();
        mockNutritionContext.Setup(x => x.MealTypes).ReturnsDbSet(dbContextMealTypes);
        mockNutritionContext
            .Setup(x => x.MealTypes.AddRangeAsync(
                It.IsAny<List<MealType>>(),
                It.IsAny<CancellationToken>()))
            .Callback<IEnumerable<MealType>, CancellationToken>((x, _) => dbContextMealTypes.AddRange(x));
        mockNutritionContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        // Act
        await MealSeedData.SeedData(mockNutritionContext.Object);

        // Assert
        dbContextMealTypes.Should().HaveCount(3);
    }
}
