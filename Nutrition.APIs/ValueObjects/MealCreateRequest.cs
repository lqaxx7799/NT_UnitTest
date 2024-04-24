namespace Nutrition.APIs;

public class MealCreateRequest
{
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public DateTimeOffset? From { get; set; }
    public DateTimeOffset? To { get; set; }
    public Guid MealTypeId { get; set; }
    public List<MealDetailCreateRequest> MealDetails { get; set; } = default!;
}

public class MealDetailCreateRequest
{
    public double Amount { get; set; }
    public string Unit { get; set; } = default!;
    public Guid FoodVariationId { get; set; }
}