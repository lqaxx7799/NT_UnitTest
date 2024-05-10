using Nutrition.Library;

namespace Feedback.APIs;

public class FeedbackResponse : BaseEntity
{
    public Guid FeedbackId { get; set; }
    public string Content { get; set; } = default!;
}
