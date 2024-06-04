using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Nutrition.Library;

namespace Nutrition.APIs;

public interface IMealService
{
    Task<List<Meal>> GetAll(MealListRequest request);
    Task<Meal> Create(MealCreateRequest request);
    Task<Meal> Update(Meal meal);
}

public class MealService : IMealService
{
    private readonly NutritionContext _nutritionContext;

    public MealService(NutritionContext nutritionContext)
    {
        _nutritionContext = nutritionContext;
    }

    public async Task<List<Meal>> GetAll(MealListRequest request)
    {
        Expression<Func<Meal, bool>> whereExpression = x => !x.IsDeleted;
        if (!string.IsNullOrEmpty(request.Keyword))
        {
            whereExpression = whereExpression.And(x => x.Title.Contains(request.Keyword, StringComparison.CurrentCultureIgnoreCase));
        }

        if (request.FromTime is not null && request.ToTime is not null)
        {
            whereExpression = whereExpression.And(x => x.From > request.FromTime && x.To < request.ToTime);
        }

        var meals = await _nutritionContext.Meals
            .Where(whereExpression)
            .ToListAsync();
        return meals;
    }

    public async Task<Meal> Create(MealCreateRequest request)
    {
        var meal = new Meal
        {
            Title = request.Title,
            Description = request.Description,
            CreatedAt = DateTimeOffset.Now,
            From = request.From,
            To = request.To,
            MealTypeId = request.MealTypeId,
            MealDetails = []
        };
        foreach (var mealDetailRequest in request.MealDetails)
        {
            var mealDetail = new MealDetail
            {
                FoodVariationId = mealDetailRequest.FoodVariationId,
                InputAmount = mealDetailRequest.Amount,
                InputUnit = mealDetailRequest.Unit,
            };
            var foodVariation = await _nutritionContext.FoodVariations
                .FirstOrDefaultAsync(x => x.Id == mealDetailRequest.FoodVariationId) ?? throw new ArgumentException("Food variation not found");
            mealDetail.DefaultUnitAmount = ConversionUtilities.Convert(mealDetailRequest.Amount, mealDetailRequest.Unit, foodVariation.NutritionServingUnit);
            meal.MealDetails.Add(mealDetail);
        }
        var entry = await _nutritionContext.Meals.AddAsync(meal);
        await _nutritionContext.SaveChangesAsync();

        return entry.Entity;
    }

    public async Task<Meal> Update(Meal meal)
    {
        var existingMeal = await _nutritionContext.Meals.FirstOrDefaultAsync(x => x.Id == meal.Id) ?? throw new Exception($"Could not find meal for {meal.Id}");
        existingMeal.Title = meal.Title;
        existingMeal.Description = meal.Description;
        existingMeal.CalculatedCalories = meal.CalculatedCalories;
        existingMeal.MealTypeId = meal.MealTypeId;
        existingMeal.From = meal.From;
        existingMeal.To = meal.To;
        _nutritionContext.Update(existingMeal);
        await _nutritionContext.SaveChangesAsync();
        return existingMeal;
    }
}
