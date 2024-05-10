using System.Text.Json.Serialization;
using Nutrition.Library;

namespace Nutrition.APIs;

public class Meal : BaseEntity
{
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public DateTimeOffset? From { get; set; }
    public DateTimeOffset? To { get; set; }
    public double CalculatedCalories { get; set; }

    public Guid MealTypeId { get; set; }

    public MealType? MealType { get; set; }

    [JsonIgnore]
    public List<MealDetail>? MealDetails { get; set; }
}
