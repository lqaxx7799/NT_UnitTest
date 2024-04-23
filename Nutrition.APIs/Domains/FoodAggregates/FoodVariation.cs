namespace Nutrition.APIs;

public class FoodVariation : BaseEntity
{
    public string VariationDescription { get; set; } = default!;
    public double NutritionServingAmount { get; set; }
    public string NutritionServingUnit { get; set; } = default!;
    public double CaloriesPerServing { get; set; }

    public Guid FoodId { get; set; }

    public Food? Food { get; set; }

    public List<FoodNutritionValue>? FoodNutritionValues { get; set; }
    public List<MealDetail>? MealDetails { get; set; }
}
