using Microsoft.EntityFrameworkCore;

namespace Nutrition.APIs;

public interface IFoodService
{
    Task<List<Food>> GetFoods(); 
}

public class FoodService : IFoodService
{
    private readonly INutritionContext _nutritionContext;

    public FoodService(INutritionContext nutritionContext)
    {
        _nutritionContext = nutritionContext;
    }

    public async Task<List<Food>> GetFoods()
    {
        return await _nutritionContext.Foods.Where(x => !x.IsDeleted).ToListAsync();
    }

}
