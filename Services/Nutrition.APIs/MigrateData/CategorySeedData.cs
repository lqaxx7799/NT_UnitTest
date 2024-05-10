using Microsoft.EntityFrameworkCore;

namespace Nutrition.APIs;

public class CategorySeedData
{
    private static readonly List<Category> _seedingCategories =
    [
        new Category
        {
            Name = "Legume",
            Description = "Legume",
            CreatedAt = DateTimeOffset.Now
        },
        new Category
        {
            Name = "Grain",
            Description = "Legume",
            CreatedAt = DateTimeOffset.Now
        },
        new Category
        {
            Name = "Fruit",
            Description = "Fruit",
            CreatedAt = DateTimeOffset.Now
        },
        new Category
        {
            Name = "Vegetable",
            Description = "Vegetable",
            CreatedAt = DateTimeOffset.Now
        },
        new Category
        {
            Name = "Meat",
            Description = "Meat",
            CreatedAt = DateTimeOffset.Now
        },
        new Category
        {
            Name = "Dairy",
            Description = "Dairy",
            CreatedAt = DateTimeOffset.Now
        },
    ];

    public static async Task SeedData(NutritionContext context)
    {
        var categoryExisted = await context.Categories.AnyAsync();
        if (!categoryExisted)
        {
            await context.Categories.AddRangeAsync(_seedingCategories);
            await context.SaveChangesAsync();
        }
    }
}
