using Microsoft.AspNetCore.Mvc;

namespace Nutrition.APIs;

public static class CategoryEndpoints
{
    public static void UseCategoryEndpoints(this WebApplication web)
    {
        var group = web.MapGroup("nutrition/category");
        group.MapGet("list", ListCategories);
        group.MapPost("create", CreateCategory);
        group.MapPut("update", UpdateCategory);
    }

    public static async Task<IResult> ListCategories([FromServices] ICategoryService categoryService)
    {
        var categories = await categoryService.GetAll();
        return Results.Ok(categories);
    }

    public static async Task<IResult> CreateCategory([FromBody] Category request, [FromServices] ICategoryService categoryService)
    {
        var category = await categoryService.Create(request);
        return Results.Ok(category);
    }

    public static async Task<IResult> UpdateCategory([FromBody] Category request, [FromServices] ICategoryService categoryService)
    {
        var result = await categoryService.Update(request);
        return Results.Ok(result);
    }
}
