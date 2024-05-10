using System.Text.Json.Serialization;
using Nutrition.Library;

namespace Nutrition.APIs;

public class MealType : BaseEntity
{
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;

    [JsonIgnore]
    public List<Meal>? Meals { get; set; }
}
