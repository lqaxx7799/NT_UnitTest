﻿using System.Text.Json.Serialization;

namespace Nutrition.APIs;

public class Food : BaseEntity
{
    public string Name { get; set; } = default!;
    [JsonIgnore]
    public List<FoodCategory>? FoodCategories { get; set; }
    [JsonIgnore]
    public List<FoodVariation>? FoodVariations { get; set; }
}
