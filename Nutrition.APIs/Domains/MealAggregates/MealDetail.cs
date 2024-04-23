﻿namespace Nutrition.APIs;

public class MealDetail : BaseEntity
{
    public double InputAmount { get; set; }
    public string InputUnit { get; set; } = default!;
    public double DefaultUnitAmount { get; set; }

    public Guid FoodId { get; set; }
    public Guid MealId { get; set; }

    public Food? Food { get; set; }
    public Meal? Meal { get; set; }
}
