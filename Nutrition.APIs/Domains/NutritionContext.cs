using Microsoft.EntityFrameworkCore;

namespace Nutrition.APIs;

public class NutritionContext : DbContext
{
    public virtual DbSet<Category> Categories { get; set; } = default!;
    public virtual DbSet<Food> Foods { get; set; } = default!;
    public virtual DbSet<FoodCategory> FoodCategories { get; set; } = default!;
    public virtual DbSet<FoodNutritionValue> FoodNutritionValues { get; set; } = default!;
    public virtual DbSet<FoodVariation> FoodVariations { get; set; } = default!;
    public virtual DbSet<Meal> Meals { get; set; } = default!;
    public virtual DbSet<MealDetail> MealDetails { get; set; } = default!;
    public virtual DbSet<MealType> MealTypes { get; set; } = default!;
    public virtual DbSet<Nutrition> Nutritions { get; set; } = default!;
    public virtual DbSet<NutritionType> NutritionTypes { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<Nutrition>()
            .HasOne(x => x.ParentNutrition)
            .WithMany(x => x.ChildrenNutritions)
            .HasForeignKey(x => x.ParentNutritionId);
    }
}
