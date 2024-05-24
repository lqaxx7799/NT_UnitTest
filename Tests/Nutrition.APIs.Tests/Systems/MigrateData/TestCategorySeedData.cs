using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;

namespace Nutrition.APIs.Tests;

public class TestCategorySeedData
{
    [Fact]
    public async Task SeedData_WhenCalled_DataInserted()
    {
        // Arrange
        var dbContextOptions = new DbContextOptions<NutritionContext>();
        var mockNutritionContext = new Mock<NutritionContext>(dbContextOptions);
        var dbContextCategories = new List<Category>();
        mockNutritionContext.Setup(x => x.Categories).ReturnsDbSet(dbContextCategories);
        mockNutritionContext
            .Setup(x => x.Categories.AddRangeAsync(
                It.IsAny<List<Category>>(),
                It.IsAny<CancellationToken>()))
            .Callback<IEnumerable<Category>, CancellationToken>((x, _) => dbContextCategories.AddRange(x));
        mockNutritionContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        // Act
        await CategorySeedData.SeedData(mockNutritionContext.Object);

        // Assert
        dbContextCategories.Should().HaveCount(6);
    }
}
