namespace Nutrition.APIs;

public class MealType : BaseEntity
{
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;

    public List<Meal>? Meals { get; set; }
}
