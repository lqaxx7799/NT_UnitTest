namespace Nutrition.APIs;

public class FoodNutritionValue : BaseEntity
{
    public double Amount { get; set; }
    public Guid? NutritionId { get; set; }
    public Guid? FoodVariationId { get; set; }

    public Nutrition? Nutrition { get; set; }
    public FoodVariation? FoodVariation { get; set; }
}
