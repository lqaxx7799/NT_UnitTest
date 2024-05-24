using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;

namespace Nutrition.APIs.Tests;

public class TestNutritionSeedData
{
    [Fact]
    public async Task SeedData_WhenCalled_DataInserted()
    {
        // Arrange
        var dbContextOptions = new DbContextOptions<NutritionContext>();
        var mockNutritionContext = new Mock<NutritionContext>(dbContextOptions);
        var dbContextNutritionTypes = new List<NutritionType>();
        mockNutritionContext.Setup(x => x.NutritionTypes).ReturnsDbSet(dbContextNutritionTypes);
        mockNutritionContext
            .Setup(x => x.NutritionTypes.AddRangeAsync(
                It.IsAny<List<NutritionType>>(),
                It.IsAny<CancellationToken>()))
            .Callback<IEnumerable<NutritionType>, CancellationToken>((x, _) => dbContextNutritionTypes.AddRange(x));

        var dbContextNutritions = new List<Nutrition>();
        mockNutritionContext.Setup(x => x.Nutritions).ReturnsDbSet(dbContextNutritions);
        mockNutritionContext
            .Setup(x => x.Nutritions.AddAsync(
                It.IsAny<Nutrition>(),
                It.IsAny<CancellationToken>()))
            .Callback<Nutrition, CancellationToken>((x, _) => dbContextNutritions.Add(x));

        mockNutritionContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        // Act
        await NutritionSeedData.SeedData(mockNutritionContext.Object);

        // Assert
        dbContextNutritionTypes.Should().HaveCount(2);
        dbContextNutritions.Should().HaveCount(3);
    }
}
