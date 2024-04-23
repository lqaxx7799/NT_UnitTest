namespace Nutrition.APIs;

public class BaseSearchRequest
{
    public string? Keyword { get; set; }
    public DateTimeOffset? FromTime { get; set; }
    public DateTimeOffset? ToTime { get; set; }
}
