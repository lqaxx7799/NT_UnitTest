using Microsoft.EntityFrameworkCore;

namespace Nutrition.APIs;

public interface ICategoryService
{
    Task<List<Category>> GetAll();
    Task<Category> Create(Category category);
    Task<Category> Update(Category category);
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
        category.CreatedAt = DateTimeOffset.Now;
        var entity = await _nutritionContext.Categories.AddAsync(category);
        await _nutritionContext.SaveChangesAsync();
        return entity.Entity;
    }

    public async Task<Category> Update(Category category)
    {
        var existingCategory = await _nutritionContext.Categories.FirstOrDefaultAsync(x => x.Id == category.Id && !x.IsDeleted)
            ?? throw new Exception($"Could not find category for {category.Id}");

        existingCategory.Description = category.Description;
        existingCategory.Name = category.Name;
        existingCategory.ModifiedAt = DateTimeOffset.Now;

        _nutritionContext.Categories.Update(existingCategory);
        await _nutritionContext.SaveChangesAsync();

        return existingCategory;
    }
}
