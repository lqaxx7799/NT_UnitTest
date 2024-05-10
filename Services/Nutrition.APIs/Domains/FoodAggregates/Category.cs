using Nutrition.Library;

namespace Nutrition.APIs;

public class Category : BaseEntity
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;

    public List<FoodCategory>? FoodCategories { get; set; }
}
