namespace Nutrition.APIs;

public class FoodCreateRequest
{
    public string Name { get; set; } = default!;
    public List<Guid> CategoryIds { get; set; } = default!;
    public List<FoodVariationCreateRequest> FoodVariations { get; set; } = default!;
}

public class FoodVariationCreateRequest
{
    public string VariationDescription { get; set; } = default!;
    public double NutritionServingAmount { get; set; }
    public string NutritionServingUnit { get; set; } = default!;
    public double CaloriesPerServing { get; set; }
    public List<FoodNutritionCreateRequest> Nutritions { get; set; } = default!;
}

public class FoodNutritionCreateRequest
{
    public Guid NutritionId { get; set; }
    public double Value { get; set; }
}