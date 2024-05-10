using Nutrition.Library;

namespace Feedback.APIs;

public class Feedback : BaseEntity
{
    public string PartitionKey { get; set; } = default!;
    public string Content { get; set; } = default!;
    public List<FeedbackAttachment>? Attachments { get; set; }
    public int RatingScore { get; set; }
}

public class FeedbackAttachment
{
    public string Url { get; set; } = default!;
    public string ContentType { get; set; } = default!;
    public string FileName { get; set; } = default!;
    public string? Description { get; set; }
}
