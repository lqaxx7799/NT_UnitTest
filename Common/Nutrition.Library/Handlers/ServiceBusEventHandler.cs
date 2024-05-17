using Microsoft.Extensions.Hosting;

namespace Nutrition.Library;

public abstract class ServiceBusEventHandler : BackgroundService
{
    public string EventName { get; set; } = default!;

    private readonly IServiceBusService _serviceBusService;

    protected ServiceBusEventHandler(IServiceBusService serviceBusService)
    {
        _serviceBusService = serviceBusService;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _serviceBusService._serviceBusClient
        throw new NotImplementedException();
    }
}

