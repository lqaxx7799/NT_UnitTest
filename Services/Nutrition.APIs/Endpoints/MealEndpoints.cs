using Microsoft.AspNetCore.Mvc;

namespace Nutrition.APIs;

public static class MealEndpoints
{
    public static void UseMealEndpoints(this WebApplication web)
    {
        var group = web.MapGroup("nutrition/meal").WithOpenApi();
        group.MapGet("list", ListMeals);
        group.MapPost("create", CreateMeal);
        group.MapPut("update", UpdateMeal);
        group.MapPost("detail/add", AddMealDetail);
        group.MapPut("detail/update", UpdateMealDetail);
    }

    public static async Task<IResult> ListMeals([AsParameters] MealListRequest request, [FromServices] IMealService mealService)
    {
        var meals = await mealService.GetAll(request);
        return Results.Ok(meals);
    }

    public static async Task<IResult> CreateMeal([FromBody] MealCreateRequest request, [FromServices] IMealService mealService)
    {
        var meal = await mealService.Create(request);
        return Results.Ok(meal);
    }

    public static async Task<IResult> UpdateMeal([FromBody] Meal request, [FromServices] IMealService mealService)
    {
        var result = await mealService.Update(request);
        return Results.Ok(result);
    }

    public static async Task<IResult> AddMealDetail([FromBody] MealDetail request, [FromServices] IMealService mealService)
    {
        var result = await mealService.AddDetail(request);
        return Results.Ok(result);
    }

    public static async Task<IResult> UpdateMealDetail([FromBody] MealDetail request, [FromServices] IMealService mealService)
    {
        var result = await mealService.UpdateDetail(request);
        return Results.Ok(result);
    }
}
