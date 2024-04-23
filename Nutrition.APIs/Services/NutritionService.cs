using Microsoft.EntityFrameworkCore;

namespace Nutrition.APIs;

public interface INutritionService
{
    Task<List<Nutrition>> GetAll();
    Task<Nutrition?> GetOne(Guid id);
    Task<Nutrition> Create(Nutrition nutrition);
}

public class NutritionService : INutritionService
{
    private readonly NutritionContext _nutritionContext;

    public NutritionService(NutritionContext nutritionContext)
    {
        _nutritionContext = nutritionContext;
    }

    public async Task<List<Nutrition>> GetAll()
    {
        return await _nutritionContext.Nutritions.Where(x => !x.IsDeleted).ToListAsync();
    }

    public async Task<Nutrition?> GetOne(Guid id)
    {
        return await _nutritionContext.Nutritions.FirstOrDefaultAsync(x => !x.IsDeleted && x.Id == id);
    }

    public async Task<Nutrition> Create(Nutrition nutrition)
    {
        var entry = await _nutritionContext.Nutritions.AddAsync(nutrition);
        await _nutritionContext.SaveChangesAsync();
        return entry.Entity;
    }
}
