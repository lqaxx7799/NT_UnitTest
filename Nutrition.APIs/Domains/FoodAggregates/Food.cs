namespace Nutrition.APIs;

public class Food : BaseEntity
{
    public string Name { get; set; } = default!;
    public List<FoodCategory>? FoodCategories { get; set; }    
}
