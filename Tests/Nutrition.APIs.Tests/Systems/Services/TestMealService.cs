using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;

namespace Nutrition.APIs.Tests;

public class TestMealService
{
    [Fact]
    public async Task GetAll_WhenCalled_ReturnsListOfMeals()
    {
        // Arrange
        var mockDbContextOptions = new DbContextOptions<NutritionContext>();
        var mockNutritionContext = new Mock<NutritionContext>(mockDbContextOptions);
        mockNutritionContext
            .Setup(c => c.Meals)
            .ReturnsDbSet(MealFixture.GetTestMeals());
        var mealListRequest = new MealListRequest();
        var sut = new MealService(mockNutritionContext.Object);

        // Act
        var foods = await sut.GetAll(mealListRequest);

        // Assert
        foods.Should().HaveCount(MealFixture.GetTestMeals().Count);
    }

    [Fact]
    public async Task GetAll_WithKeywordMatchCase_ReturnFilteredList()
    {
        // Arrange
        var mockDbContextOptions = new DbContextOptions<NutritionContext>();
        var mockNutritionContext = new Mock<NutritionContext>(mockDbContextOptions);
        mockNutritionContext
            .Setup(c => c.Meals)
            .ReturnsDbSet(MealFixture.GetTestMeals());
        var mealListRequest = new MealListRequest
        {
            Keyword = "Lunch"
        };
        var sut = new MealService(mockNutritionContext.Object);

        // Act
        var foods = await sut.GetAll(mealListRequest);

        // Assert
        foods.Should().HaveCount(1);
    }

    [Fact]
    public async Task GetAll_WithKeywordIgnoredCase_ReturnFilteredList()
    {
        // Arrange
        var mockDbContextOptions = new DbContextOptions<NutritionContext>();
        var mockNutritionContext = new Mock<NutritionContext>(mockDbContextOptions);
        mockNutritionContext
            .Setup(c => c.Meals)
            .ReturnsDbSet(MealFixture.GetTestMeals());
        var mealListRequest = new MealListRequest
        {
            Keyword = "lunc"
        };
        var sut = new MealService(mockNutritionContext.Object);

        // Act
        var foods = await sut.GetAll(mealListRequest);

        // Assert
        foods.Should().HaveCount(1);
    }

    [Fact]
    public async Task GetAll_WithTimeRangeIn20240321_ReturnFilteredList()
    {
        // Arrange
        var mockDbContextOptions = new DbContextOptions<NutritionContext>();
        var mockNutritionContext = new Mock<NutritionContext>(mockDbContextOptions);
        mockNutritionContext
            .Setup(c => c.Meals)
            .ReturnsDbSet(MealFixture.GetTestMeals());
        var mealListRequest = new MealListRequest
        {
            FromTime = new DateTimeOffset(2024, 3, 21, 0, 0, 0, TimeSpan.Zero),
            ToTime = new DateTimeOffset(2024, 3, 22, 0, 0, 0, TimeSpan.Zero).AddTicks(-1),
        };
        var sut = new MealService(mockNutritionContext.Object);

        // Act
        var foods = await sut.GetAll(mealListRequest);

        // Assert
        foods.Should().HaveCount(1);
    }
}
