namespace Storage.APIs;

public static class HealthEndpoints
{
    public static void UseHealthEndpoints(this WebApplication web)
    {
        var group = web.MapGroup("storage");
        group.MapGet("health", () =>
        {
            return Results.Ok("Healthy!");
        });
    }
}
