using Microsoft.Azure.Cosmos.Linq;

namespace Feedback.APIs;

public interface IFeedbackService
{
    Task<List<Feedback>> GetAll();
    Task<Feedback> Create(Feedback feedback);
}

public class FeedbackService : CosmoService<Feedback>, IFeedbackService
{
    public FeedbackService(IConfiguration configuration) : base(configuration)
    {}

    
    public async Task<List<Feedback>> GetAll()
    {
        var queryable = _container.GetItemLinqQueryable<Feedback>();
        using var feed = queryable
            .Where(x => !x.IsDeleted)
            .ToFeedIterator();
        var results = new List<Feedback>();
        while (feed.HasMoreResults)
        {
            var response = await feed.ReadNextAsync();
            results.AddRange(response);
        }
        return results;
    }

    public async Task<Feedback> Create(Feedback feedback)
    {
        var response = await _container.CreateItemAsync(feedback);
        return response;
    }
}
