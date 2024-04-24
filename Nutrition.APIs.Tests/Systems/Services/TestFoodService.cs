using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;

namespace Nutrition.APIs.Tests;

public class TestFoodService
{
    [Fact]
    public async Task GetAll_WhenCalled_ReturnsListOfFoods()
    {
        // Arrange
        var mockDbContextOptions = new DbContextOptions<NutritionContext>();
        var mockNutritionContext = new Mock<NutritionContext>(mockDbContextOptions);
        mockNutritionContext
            .Setup(c => c.Foods)
            .ReturnsDbSet(FoodFixture.GetTestFoods());

        var sut = new FoodService(mockNutritionContext.Object);

        // Act
        var foods = await sut.GetAll();

        // Assert
        foods.Should().HaveCount(FoodFixture.GetTestFoods().Count);
    }

    [Theory]
    [InlineData("62FA647C-AD54-4BCC-A860-E5A2664B019D")]
    [InlineData("7B1F3580-63EB-4686-8588-8BAD339D48A5")]
    public async Task GetOne_FindsOne_ReturnsFood(string id)
    {
        // Arrange
        var foodId = Guid.Parse(id);
        var mockDbContextOptions = new DbContextOptions<NutritionContext>();
        var mockNutritionContext = new Mock<NutritionContext>(mockDbContextOptions);
        mockNutritionContext
            .Setup(c => c.Foods)
            .ReturnsDbSet(FoodFixture.GetTestFoods());

        var sut = new FoodService(mockNutritionContext.Object);

        // Act
        var food = await sut.GetOne(foodId);

        // Assert
        food.Should().NotBeNull()
            .And.BeOfType<Food>()
            .Which.Id.Should().Be(foodId);
    }

    [Theory]
    [InlineData("54A74DB1-497E-4467-80C3-5392BA7CBE7E")]
    public async Task GetOne_DoesNotFindAnything_ReturnsNull(string id)
    {
        // Arrange
        var foodId = Guid.Parse(id);
        var mockDbContextOptions = new DbContextOptions<NutritionContext>();
        var mockNutritionContext = new Mock<NutritionContext>(mockDbContextOptions);
        mockNutritionContext
            .Setup(c => c.Foods)
            .ReturnsDbSet(FoodFixture.GetTestFoods());

        var sut = new FoodService(mockNutritionContext.Object);

        // Act
        var food = await sut.GetOne(foodId);

        // Assert
        food.Should().BeNull();
    }

    [Fact]
    public async Task Create_WhenCalled_CallsAddAsync()
    {
        // Arrange
        var request = new FoodCreateRequest
        {
            FoodVariations = [],
            CategoryIds = [],
            Name = "Test"
        };
        var mockDbContextOptions = new DbContextOptions<NutritionContext>();
        var mockNutritionContext = new Mock<NutritionContext>(mockDbContextOptions);
        mockNutritionContext
            .Setup(c => c.Foods)
            .ReturnsDbSet(FoodFixture.GetTestFoods());

        mockNutritionContext
            .Setup(c => c.Categories)
            .ReturnsDbSet([]);

        var sut = new FoodService(mockNutritionContext.Object);

        // Act
        var food = await sut.Create(request);

        // Assert
        food.Should().BeOfType<Food>();
    }
}
