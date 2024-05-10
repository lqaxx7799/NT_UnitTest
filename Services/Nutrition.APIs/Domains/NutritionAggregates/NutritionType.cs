using Nutrition.Library;

namespace Nutrition.APIs;

public class NutritionType : BaseEntity
{
    public string Name { get; set; } = default!;

    public List<Nutrition>? Nutritions { get; set; }
}
