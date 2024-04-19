using Microsoft.EntityFrameworkCore;

namespace Nutrition.APIs;

public interface INutritionContext
{
    DbSet<Category> Categories { get; set; }
    DbSet<Food> Foods { get; set; }
    DbSet<FoodCategory> FoodCategories { get; set; }
    DbSet<FoodNutritionValue> FoodNutritionValues { get; set; }
    DbSet<Nutrition> Nutritions { get; set; }
}

public class NutritionContext : DbContext, INutritionContext
{
    public string DbPath { get; }

    public NutritionContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = Path.Join(path, "nutrition.db");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");

    public DbSet<Category> Categories { get; set; } = default!;
    public DbSet<Food> Foods { get; set; } = default!;
    public DbSet<FoodCategory> FoodCategories { get; set; } = default!;
    public DbSet<FoodNutritionValue> FoodNutritionValues { get; set; } = default!;
    public DbSet<Nutrition> Nutritions { get; set; } = default!;
}
