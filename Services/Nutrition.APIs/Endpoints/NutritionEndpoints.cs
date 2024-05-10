using Microsoft.AspNetCore.Mvc;

namespace Nutrition.APIs;

public static class NutritionEndpoints
{
    public static void UseNutritionEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("nutrition").WithOpenApi();

        group.MapGet("list", ListNutritions);
        group.MapGet("get", GetOne);
        group.MapPost("create", Create);
    }

    public static async Task<IResult> ListNutritions([FromServices] INutritionService nutritionService)
    {
        var nutritions = await nutritionService.GetAll();
        return Results.Ok(nutritions);
    }

    public static async Task<IResult> GetOne([FromQuery] Guid id, [FromServices] INutritionService nutritionService)
    {
        var nutrition = await nutritionService.GetOne(id);
        if (nutrition is null)
        {
            return Results.NotFound();
        }

        return Results.Ok(nutrition);
    }

    public static async Task<IResult> Create([FromBody] Nutrition request, [FromServices] INutritionService nutritionService)
    {
        var nutrition = await nutritionService.Create(request);
        return Results.Ok(nutrition);
    }
}
