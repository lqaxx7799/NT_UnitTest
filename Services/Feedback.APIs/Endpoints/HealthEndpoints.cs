namespace Feedback.APIs;

public static class HealthEndpoints
{
    public static void UseHealthEndpoints(this WebApplication web)
    {
        var group = web.MapGroup("feedback");
        group.MapGet("health", () =>
        {
            return Results.Ok("Healthy!");
        });
    }
}
