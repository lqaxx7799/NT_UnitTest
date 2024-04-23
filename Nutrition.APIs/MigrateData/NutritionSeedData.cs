using Microsoft.EntityFrameworkCore;

namespace Nutrition.APIs;

public class NutritionSeedData
{

    private static readonly List<NutritionType> _seedingNutritionTypes =
    [
        new NutritionType
        {
            Name = "Macronutrients"
        },
        new NutritionType
        {
            Name = "Micronutrients"
        },
    ];

    private static readonly List<Nutrition> _seedingNutritions =
    [
        new Nutrition
        {
            Name = "Fat",
            Unit = "g",
            CaloriesPerUnit = 9,
            Description = "",
            CreatedAt = DateTimeOffset.Now,
            NutritionTypeValue = "Macronutrients",
            ChildrenNutritions =
            [
                new Nutrition
                {
                    Name = "Saturated Fat",
                    Unit = "g",
                    CaloriesPerUnit = 9,
                    Description = "",
                    CreatedAt = DateTimeOffset.Now,
                },
                new Nutrition
                {
                    Name = "Monounsaturated Fat",
                    Unit = "g",
                    CaloriesPerUnit = 9,
                    Description = "",
                    CreatedAt = DateTimeOffset.Now,
                },
                new Nutrition
                {
                    Name = "Polyunsaturated Fat",
                    Unit = "g",
                    CaloriesPerUnit = 9,
                    Description = "",
                    CreatedAt = DateTimeOffset.Now,
                    ChildrenNutritions =
                    [
                        new Nutrition
                        {
                            Name = "Linoleic Acid",
                            Unit = "g",
                            CaloriesPerUnit = 9,
                            Description = "Omega-6 Fatty Acid",
                            CreatedAt = DateTimeOffset.Now,
                        },
                        new Nutrition
                        {
                            Name = "Alpha Linolenic Acid (ALA)",
                            Unit = "g",
                            CaloriesPerUnit = 9,
                            Description = "Omega-3 Fatty Acid",
                            CreatedAt = DateTimeOffset.Now,
                        },
                        new Nutrition
                        {
                            Name = "Eicosapentaenoic Acid (EPA)",
                            Unit = "g",
                            CaloriesPerUnit = 9,
                            Description = "Omega-3 Fatty Acid",
                            CreatedAt = DateTimeOffset.Now,
                        },
                        new Nutrition
                        {
                            Name = "Docosahexaenoic Acid (DHA)",
                            Unit = "g",
                            CaloriesPerUnit = 9,
                            Description = "Omega-3 Fatty Acid",
                            CreatedAt = DateTimeOffset.Now,
                        },
                    ]
                },
                new Nutrition
                {
                    Name = "Trans Fat",
                    Unit = "g",
                    CaloriesPerUnit = 9,
                    Description = "",
                    CreatedAt = DateTimeOffset.Now,
                }
            ]
        },
        new Nutrition
        {
            Name = "Carbohydrate",
            Unit = "g",
            CaloriesPerUnit = 4,
            Description = "",
            CreatedAt = DateTimeOffset.Now,
            NutritionTypeValue = "Macronutrients",
            ChildrenNutritions =
            [
                new Nutrition
                {
                    Name = "Sugars",
                    Unit = "g",
                    CaloriesPerUnit = 0,
                    Description = "",
                    CreatedAt = DateTimeOffset.Now,
                },
                new Nutrition
                {
                    Name = "Dietary Fiber",
                    Unit = "g",
                    CaloriesPerUnit = 0,
                    Description = "",
                    CreatedAt = DateTimeOffset.Now,
                }
            ]
        },
        new Nutrition
        {
            Name = "Protein",
            Unit = "g",
            CaloriesPerUnit = 4,
            Description = "",
            CreatedAt = DateTimeOffset.Now,
            NutritionTypeValue = "Macronutrients",
        }
    ];

    public static async Task SeedData(NutritionContext context)
    {
        var nutritionTypeExisted = await context.NutritionTypes.AnyAsync();
        if (!nutritionTypeExisted)
        {
            await context.NutritionTypes.AddRangeAsync(_seedingNutritionTypes);
            await context.SaveChangesAsync();
        }

        var nutritionExisted = await context.Nutritions.AnyAsync();
        if (!nutritionExisted)
        {
            foreach (var nutrition in _seedingNutritions)
            {
                nutrition.NutritionTypeId = _seedingNutritionTypes.FirstOrDefault(x => x.Name == nutrition.Name)?.Id;
                await context.Nutritions.AddAsync(nutrition);
            }

            await context.SaveChangesAsync();
        }
    }
}
