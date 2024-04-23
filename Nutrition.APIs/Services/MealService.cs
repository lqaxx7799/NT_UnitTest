using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Nutrition.Library;

namespace Nutrition.APIs;

public interface IMealService
{
    Task<List<Meal>> GetAll(MealListRequest request);
    Task<Meal> Create(MealCreateRequest request);
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
        if (string.IsNullOrEmpty(request.Keyword))
        {
            whereExpression = whereExpression.And(x => x.Title == request.Keyword);
        }

        if (request.FromTime is not null && request.ToTime is not null)
        {
            whereExpression = whereExpression.And(x => x.CreatedAt > request.FromTime && x.CreatedAt < request.ToTime);
        }

        var meals = await _nutritionContext.Meals
            .Where(whereExpression)
            .ToListAsync();
        return meals;
    }

    public Task<Meal> Create(MealCreateRequest request)
    {
        return null;
    }

}
