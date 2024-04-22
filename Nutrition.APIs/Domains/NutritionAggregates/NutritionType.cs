namespace Nutrition.APIs;

public class NutritionType
{
    public string Name { get; set; } = default!;

    public List<Nutrition>? Nutritions { get; set; }
}
