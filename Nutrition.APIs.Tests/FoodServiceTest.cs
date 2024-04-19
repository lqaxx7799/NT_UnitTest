using Moq;
using MockQueryable.Moq;

namespace Nutrition.APIs.Tests;

public class FoodServiceTest
{
    private readonly IFoodService _foodService;

    protected Mock<INutritionContext> MockContext;

    public FoodServiceTest()
    {
        List<Food> foods =
        [
            new Food { Name = "Egg" },
            new Food { Name = "Chicken" },
            new Food { Name = "Pork" },
        ];
        var mock = foods.AsQueryable().BuildMockDbSet();
        MockContext = new Mock<INutritionContext>();
        MockContext
               .Setup(c => c.Foods)
               .Returns(mock.Object);
        _foodService = new FoodService(MockContext.Object);
    }

    [Fact]
    public async Task GetFoods_ReturnsList()
    {
        // arrange

        // act
        var actual = await _foodService.GetFoods();

        // assert
        Assert.Equal(3, actual.Count);
    }
}
