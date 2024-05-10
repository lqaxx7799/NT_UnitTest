using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Nutrition.Library;

namespace Nutrition.APIs;

public class Nutrition : BaseEntity
{
    public string Name { get; set; } = default!;
    public string Unit { get; set; } = default!;
    public string? Description { get; set; }
    public double CaloriesPerUnit { get; set; }

    public Guid? ParentNutritionId { get; set; }
    public Guid? NutritionTypeId { get; set; }

    [JsonIgnore]
    public Nutrition? ParentNutrition { get; set; }
    public NutritionType? NutritionType { get; set; }

    [JsonIgnore]
    public List<Nutrition>? ChildrenNutritions { get; set; }
    [JsonIgnore]
    public List<FoodNutritionValue>? FoodNutritionValues { get; set; }

    [NotMapped]
    public string? NutritionTypeValue { get; set; }
}
