﻿using Microsoft.EntityFrameworkCore;

namespace Nutrition.APIs;

public interface IFoodService
{
    Task<List<Food>> GetAll(); 
    Task<Food?> GetOne(Guid id);
    Task<Food> Create(FoodCreateRequest foodCreateRequest);
}

public class FoodService : IFoodService
{
    private readonly NutritionContext _nutritionContext;

    public FoodService(NutritionContext nutritionContext)
    {
        _nutritionContext = nutritionContext;
    }

    public async Task<List<Food>> GetAll()
    {
        return await _nutritionContext.Foods.Where(x => !x.IsDeleted).ToListAsync();
    }

    public async Task<Food?> GetOne(Guid id)
    {
        return await _nutritionContext.Foods.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
    }

    public async Task<Food> Create(FoodCreateRequest foodCreateRequest)
    {
        var food = new Food
        {
            Name = foodCreateRequest.Name,
            CreatedAt = DateTimeOffset.Now,
            ModifiedAt = null,
            FoodCategories = foodCreateRequest.CategoryIds.Select(x => new FoodCategory
            {
                CategoryId = x,
                CreatedAt = DateTimeOffset.Now
            }).ToList(),
            FoodVariations = [],
        };
        var nutritions = await _nutritionContext.Nutritions.Where(x => !x.IsDeleted).ToListAsync();

        foreach (var foodVariationRequest in foodCreateRequest.FoodVariations)
        {
            var foodVariation = new FoodVariation
            {
                CaloriesPerServing = foodVariationRequest.CaloriesPerServing,
                NutritionServingAmount = foodVariationRequest.NutritionServingAmount,
                NutritionServingUnit = foodVariationRequest.NutritionServingUnit,
                VariationDescription = foodVariationRequest.VariationDescription,
                CreatedAt = DateTimeOffset.Now,
                FoodNutritionValues = foodVariationRequest.Nutritions.Select(x => new FoodNutritionValue
                {
                    NutritionId = x.NutritionId,
                    Amount = ConversionUtilities.Convert(x.Amount, x.Unit, nutritions.FirstOrDefault(nutrition => nutrition.Id == x.NutritionId)?.Unit!),
                    CreatedAt = DateTimeOffset.Now,
                }).ToList()
            };
            food.FoodVariations.Add(foodVariation);
        }
        await _nutritionContext.Foods.AddAsync(food);
        await _nutritionContext.SaveChangesAsync();
        return food;
    }
}