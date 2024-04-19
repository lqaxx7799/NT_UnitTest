namespace Nutrition.APIs;

public class FoodNutritionValue : BaseEntity
{
    public double Value { get; set; }
    public Guid? NutritionId { get; set; }
    public Guid? FoodId { get; set; }

    public Nutrition? Nutrition { get; set; }
    public Food? Food { get; set; }
}
