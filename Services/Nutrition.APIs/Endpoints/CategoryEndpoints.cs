using Microsoft.AspNetCore.Mvc;

namespace Nutrition.APIs;

public static class CategoryEndpoints
{
    public static void UseCategoryEndpoints(this WebApplication web)
    {
        var group = web.MapGroup("category");
        group.MapGet("list", ListCategories);
    }

    public static async Task<IResult> ListCategories([FromServices] ICategoryService categoryService)
    {
        var categories = await categoryService.GetAll();
        return Results.Ok(categories);
    }
}
