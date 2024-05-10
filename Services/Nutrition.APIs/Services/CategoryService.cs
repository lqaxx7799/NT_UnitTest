using Microsoft.EntityFrameworkCore;

namespace Nutrition.APIs;

public interface ICategoryService
{
    Task<List<Category>> GetAll();
}

public class CategoryService : ICategoryService
{
    private readonly NutritionContext _nutritionContext;

    public CategoryService(NutritionContext nutritionContext)
    {
        _nutritionContext = nutritionContext;
    }

    public async Task<List<Category>> GetAll()
    {
        return await _nutritionContext.Categories.Where(x => !x.IsDeleted).ToListAsync();
    }

}
