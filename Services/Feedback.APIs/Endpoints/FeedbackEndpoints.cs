using Microsoft.AspNetCore.Mvc;

namespace Feedback.APIs;

public static class FeedbackEndpoints
{
    public static void UseFeedbackEndpoints(this WebApplication web)
    {
        var group = web.MapGroup("feedback").WithOpenApi();
        group.MapGet("list", ListFeedbacks);
        group.MapPost("create", CreateFeedback);

    }

    public static async Task<IResult> ListFeedbacks([FromServices] IFeedbackService feedbackService)
    {
        var feedbacks = await feedbackService.GetAll();
        return Results.Ok(feedbacks);
    }

    public static async Task<IResult> CreateFeedback([FromBody] Feedback request, [FromServices] IFeedbackService feedbackService)
    {
        var feedback = await feedbackService.Create(request);
        return Results.Ok(feedback);
    }
}
