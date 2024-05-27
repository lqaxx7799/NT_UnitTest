namespace Nutrition.APIs;

public static class HealthEndpoints
{
    public static void UseHealthEndpoints(this WebApplication web)
    {
        var group = web.MapGroup("nutrition");
        group.MapGet("health", () =>
        {
            return Results.Ok("Healthy!");
        });
    }
}