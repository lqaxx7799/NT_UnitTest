using System.Text.Json;
using MassTransit;
using Nutrition.Library;

namespace Feedback.APIs;

public class NutritionCreatedConsumer : IConsumer<NutritionCreatedEvent>
{
    public async Task Consume(ConsumeContext<NutritionCreatedEvent> context)
    {
        Console.WriteLine("Ok!: " + JsonSerializer.Serialize(context.Message));
    }
}
