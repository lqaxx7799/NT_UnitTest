namespace Nutrition.APIs;

public class MealCreateRequest
{
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public DateTimeOffset? From { get; set; }
    public DateTimeOffset? To { get; set; }
    public double CalculatedCalories { get; set; }
    public Guid MealTypeId { get; set; }
}
