namespace Nutrition.APIs;

public class Nutrition
{
    public string Name { get; set; } = default!;
    public string Unit { get; set; } = default!;
    public Guid? ParentNutritionId { get; set; }

    public Nutrition? ParentNutrition { get; set; }

    public List<Nutrition>? Nutritions { get; set; }
}
