namespace Nutrition.APIs;

public class FoodVariation
{
    public string VariationDescription { get; set; } = default!;
    public double NutritionServingAmount { get; set; }
    public string NutritionServingUnit { get; set; } = default!;
    public double CaloriesPerServing { get; set; }

    public List<FoodNutritionValue>? FoodNutritionValues { get; set; }
}
