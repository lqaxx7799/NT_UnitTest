using Microsoft.AspNetCore.Mvc;

namespace Nutrition.APIs;

public static class MealEndpoints
{
    public static void UseMealEndpoints(this WebApplication web)
    {
        var group = web.MapGroup("meal").WithOpenApi();
        group.MapGet("list", ListMeals);
        group.MapGet("create", CreateMeal);
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
}
