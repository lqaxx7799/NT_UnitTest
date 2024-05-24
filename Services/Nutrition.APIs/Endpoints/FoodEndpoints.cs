using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Nutrition.APIs;

public static class FoodEndpoints
{
    public static void UseFoodEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("nutrition/food").WithOpenApi();

        group.MapGet("list", ListFoods);

        group.MapGet("get", GetFood);

        group.MapPost("create", CreateFood);

        group.MapPut("update", UpdateFood);

        group.MapDelete("delete", DeleteFood);
    }

    public static async Task<IResult> ListFoods([FromServices] IFoodService foodService)
    {
        var foods = await foodService.GetAll();
        return Results.Ok(foods);
    }

    public static async Task<IResult> GetFood([FromQuery] Guid id, [FromServices] IFoodService foodService)
    {
        var food = await foodService.GetOne(id);
        if (food is null)
        {
            return Results.NotFound();
        }

        return Results.Ok(food);
    }

    public static async Task<IResult> CreateFood([FromBody] FoodCreateRequest request, [FromServices] IFoodService foodService)
    {
        var food = await foodService.Create(request);
        return Results.Ok(food);
    }

    public static async Task<IResult> UpdateFood([FromBody] Food food, [FromServices] NutritionContext nutritionContext)
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
    }

    public static async Task<IResult> DeleteFood([FromQuery] Guid id, [FromServices] NutritionContext nutritionContext)
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
    }

}
