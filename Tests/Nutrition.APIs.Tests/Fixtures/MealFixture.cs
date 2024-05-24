namespace Nutrition.APIs.Tests;

public class MealFixture
{
    public static List<Meal> GetTestMeals() =>
    [
        new Meal
        {
            Id = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D"),
            Title = "Breakfast",
            Description = "Breakfast",
            From = new DateTimeOffset(2024, 5, 3, 6, 0, 0, TimeSpan.Zero),
            To = new DateTimeOffset(2024, 5, 3, 7, 0, 0, TimeSpan.Zero)
        },
        new Meal
        {
            Id = new Guid("7B1F3580-63EB-4686-8588-8BAD339D48A5"),
            Title = "Lunch",
            Description = "Lunch",
            From = new DateTimeOffset(2024, 3, 21, 11, 0, 0, TimeSpan.Zero),
            To = new DateTimeOffset(2024, 3, 21, 12, 30, 0, TimeSpan.Zero)
        },
        new Meal
        {
            Id = new Guid("9A3BCF12-1CD5-4930-83A2-2AA816A4BF75"),
            Title = "Dinner",
            Description = "Dinner",
            From = new DateTimeOffset(2024, 4, 17, 18, 0, 0, TimeSpan.Zero),
            To = new DateTimeOffset(2024, 4, 17, 20, 0, 0, TimeSpan.Zero)
        },
    ];
}
