using Microsoft.EntityFrameworkCore;

namespace Nutrition.APIs;

public interface ICategoryService
{
    Task<List<Category>> GetAll();
    Task<Category> Create(Category category);
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

    public async Task<Category> Create(Category category)
    {
        var entity = await _nutritionContext.Categories.AddAsync(category);
        await _nutritionContext.SaveChangesAsync();
        return entity.Entity;
    }
}
