using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Nutrition.APIs;

public static class FoodEndpoints
{
    public static void UseFoodEndpoins(this WebApplication app)
    {
        var group = app.MapGroup("food").WithOpenApi();

        group.MapGet("list", async ([FromServices] IFoodService foodService) =>
        {
            var foods = await foodService.GetFoods();
            return Results.Ok(foods);
        });

        group.MapGet("get", async ([FromQuery] Guid id, [FromServices] NutritionContext nutritionContext) =>
        {
            var food = await nutritionContext.Foods.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
            return Results.Ok(food);
        });

        group.MapPost("add", async ([FromBody] Food food, [FromServices] NutritionContext nutritionContext) =>
        {
            food.CreatedAt = DateTimeOffset.Now;
            food.ModifiedAt = null;
            await nutritionContext.Foods.AddAsync(food);
            await nutritionContext.SaveChangesAsync();
            return Results.Ok(food);
        });

        group.MapPut("update", async ([FromBody] Food food, [FromServices] NutritionContext nutritionContext) =>
        {
            var existingFood = await nutritionContext.Foods.FirstOrDefaultAsync(x => x.Id == food.Id && !x.IsDeleted);
            if (existingFood is null)
            {
                return Results.BadRequest("Resource does not exist");
            }

            existingFood.Name = food.Name;
            existingFood.ModifiedAt = DateTimeOffset.Now;

            nutritionContext.Foods.Update(existingFood);
            await nutritionContext.SaveChangesAsync();
            return Results.Ok(existingFood);
        });

        group.MapDelete("delete", async ([FromQuery] Guid id, [FromServices] NutritionContext nutritionContext) =>
        {
            var existingFood = await nutritionContext.Foods.FirstOrDefaultAsync(x => x.Id == id);
            if (existingFood is null)
            {
                return Results.BadRequest("Resource does not exist");
            }

            existingFood.IsDeleted = true;
            existingFood.DeletedAt = DateTimeOffset.Now;

            nutritionContext.Foods.Update(existingFood);
            await nutritionContext.SaveChangesAsync();
            return Results.Ok();
        });
    }

}
