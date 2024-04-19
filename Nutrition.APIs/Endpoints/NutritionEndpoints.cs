namespace Nutrition.APIs;

public static class NutritionEndpoints
{
    public static void UseNutritionEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("nutrition").WithOpenApi();

        // group.MapPost("")
    }
}
