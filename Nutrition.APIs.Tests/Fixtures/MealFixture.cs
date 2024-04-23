namespace Nutrition.APIs.Tests;

public class MealFixture
{
    public static List<Meal> GetTestMeals() =>
    [
        new Meal
        {
            Id = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D"),
            Title = "Breakfast",
            Description = "Breakfast"
        },
        new Meal
        {
            Id = new Guid("7B1F3580-63EB-4686-8588-8BAD339D48A5"),
            Title = "Lunch",
            Description = "Lunch"
        },
        new Meal
        {
            Id = new Guid("9A3BCF12-1CD5-4930-83A2-2AA816A4BF75"),
            Title = "Dinner",
            Description = "Dinner"
        },
    ];
}
