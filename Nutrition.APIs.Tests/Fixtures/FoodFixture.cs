namespace Nutrition.APIs.Tests;

public static class FoodFixture
{
    public static List<Food> GetTestFoods() =>
    [
        new Food
        {
            Id = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D"),
            Name = "Egg",
        },
        new Food
        {
            Id = new Guid("7B1F3580-63EB-4686-8588-8BAD339D48A5"),
            Name = "Chicken"
        },
        new Food
        {
            Id = new Guid("9A3BCF12-1CD5-4930-83A2-2AA816A4BF75"),
            Name = "Pork"
        },
    ];
}
