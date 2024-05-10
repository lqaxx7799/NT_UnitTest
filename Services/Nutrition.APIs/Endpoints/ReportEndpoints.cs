using Microsoft.AspNetCore.Mvc;

namespace Nutrition.APIs;

public static class ReportEndpoints
{
    public static void UseReportEndpoints(this IEndpointRouteBuilder web)
    {
        var group = web.MapGroup("report").WithOpenApi();
        group.MapGet("nutritionProfile", GetNutritionProfileReport);
    }

    public static async Task<IResult> GetNutritionProfileReport([AsParameters] ReportNutritionProfileRequest request, [FromServices] IReportService reportService)
    {
        var result = await reportService.GetNutritionProfile(request);
        return Results.Ok(result);
    }
}
