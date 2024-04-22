using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Nutrition.APIs;

public static class FoodEndpoints
{
    public static void UseFoodEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("food").WithOpenApi();

        group.MapGet("list", ListFoods);

        group.MapGet("get", GetFood);

        group.MapPost("create", async ([FromBody] FoodCreateRequest request, [FromServices] NutritionContext nutritionContext) =>
        {
            var food = new Food
            {
                Name = request.Name,
                CreatedAt = DateTimeOffset.Now,
                ModifiedAt = null
            };
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

    public static async Task<IResult> ListFoods([FromServices] IFoodService foodService)
    {
        var foods = await foodService.GetFoods();
        return Results.Ok(foods);
    }

    public static async Task<IResult> GetFood([FromQuery] Guid id, [FromServices] IFoodService foodService)
    {
        var food = await foodService.GetFood(id);
        if (food is null) 
        {
            return Results.NotFound();
        }

        return Results.Ok(food);
    }

}
