using Microsoft.EntityFrameworkCore;

namespace Nutrition.APIs;

public class MealSeedData
{
    private static readonly List<MealType> _seedingMealTypes =
    [
        new MealType
        {
            Title = "Breakfast",
            Description = "Breakfast",
            CreatedAt = DateTimeOffset.Now
        },
        new MealType
        {
            Title = "Lunch",
            Description = "Lunch",
            CreatedAt = DateTimeOffset.Now
        },
        new MealType
        {
            Title = "Dinner",
            Description = "Dinner",
            CreatedAt = DateTimeOffset.Now
        }
    ];

    public static async Task SeedData(NutritionContext context)
    {
        var mealTypeExisted = await context.MealTypes.AnyAsync();
        if (!mealTypeExisted)
        {
            await context.MealTypes.AddRangeAsync(_seedingMealTypes);
            await context.SaveChangesAsync();
        }
    }
}
