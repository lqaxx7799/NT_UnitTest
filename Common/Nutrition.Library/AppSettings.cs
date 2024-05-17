namespace Nutrition.Library;

public class AppSettings
{
    public Dictionary<string, string> ConnectionStrings { get; set; } = default!;
    public AzureServiceBusConfiguration AzureServiceBusConfiguration { get; set; } = default!;
}

public class AzureServiceBusConfiguration
{
    public string ConnectionString { get; set; } = default!;
    public string QueueName { get; set; } = default!;
}
