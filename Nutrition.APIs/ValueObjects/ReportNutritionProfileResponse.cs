namespace Nutrition.APIs;

public class ReportNutritionProfileResponse
{
    public DateTimeOffset? FromTime { get; set; }
    public DateTimeOffset? ToTime { get; set; }
    public double TotalCalories { get; set; }
    public List<ReportNutritionValue> NutritionValues { get; set; } = default!;
}

public class ReportNutritionValue
{
    public Guid NutritionId { get; set; }
    public string NutritionName { get; set; } = default!;
    public string Unit { get; set; } = default!;
    public double Amount { get; set; } = default!;
    public List<ReportNutritionValue>? Nutritions { get; set; }
}