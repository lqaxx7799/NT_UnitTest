using Microsoft.EntityFrameworkCore;

namespace Nutrition.APIs;

public interface IReportService
{
    Task<ReportNutritionProfileResponse> GetNutritionProfile(ReportNutritionProfileRequest request);
}

public class ReportService : IReportService
{
    private readonly NutritionContext _nutritionContext;

    public ReportService(NutritionContext nutritionContext)
    {
        _nutritionContext = nutritionContext;
    }

    public async Task<ReportNutritionProfileResponse> GetNutritionProfile(ReportNutritionProfileRequest request)
    {
        var nutritions = await _nutritionContext.Nutritions.ToListAsync();

        var meals = await _nutritionContext.Meals
            .Include(x => x.MealDetails)
            .Where(x => x.From >= request.FromTime && x.To <= request.ToTime)
            .ToListAsync();

        var reportNutritionProfileResponse = new ReportNutritionProfileResponse
        {
            TotalCalories = 0,
            NutritionValues = [],
            FromTime = request.FromTime,
            ToTime = request.ToTime,
        };

        foreach (var meal in meals)
        {
            if (meal.MealDetails is null)
            {
                continue;
            }

            foreach (var mealDetail in meal.MealDetails)
            {
                var foodVariation = await _nutritionContext.FoodVariations
                    .Include(x => x.FoodNutritionValues)
                    .FirstOrDefaultAsync(x => x.Id == mealDetail.FoodVariationId);
                if (foodVariation?.FoodNutritionValues is null)
                {
                    continue;
                }
                reportNutritionProfileResponse.TotalCalories += foodVariation.CaloriesPerServing * mealDetail.DefaultUnitAmount / foodVariation.NutritionServingAmount;
                foreach (var foodNutritionValue in foodVariation.FoodNutritionValues)
                {
                    var nutrition = nutritions.FirstOrDefault(x => x.Id == foodNutritionValue.NutritionId);
                    if (nutrition is null)
                    {
                        continue;
                    }

                    var reportNutritionValue = reportNutritionProfileResponse.NutritionValues.FirstOrDefault(x => x.NutritionId == nutrition.Id);
                    if (reportNutritionValue is null)
                    {
                        reportNutritionValue = new ReportNutritionValue
                        {
                            Amount = 0,
                            NutritionId = nutrition.Id,
                            NutritionName = nutrition.Name,
                            Unit = nutrition.Unit,
                        };
                        reportNutritionProfileResponse.NutritionValues.Add(reportNutritionValue);
                    }
                    reportNutritionValue.Amount += foodNutritionValue.Amount * mealDetail.DefaultUnitAmount / foodVariation.NutritionServingAmount;
                }
            }
        }
        
        return reportNutritionProfileResponse;
    }
}
