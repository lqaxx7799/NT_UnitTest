using Microsoft.EntityFrameworkCore;

namespace Nutrition.APIs;

public interface IFoodService
{
    Task<List<Food>> GetFoods(); 
    Task<Food?> GetFood(Guid id);
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

    public async Task<Food?> GetFood(Guid id)
    {
        return await _nutritionContext.Foods.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
    }

}
