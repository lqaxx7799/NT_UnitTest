using Azure.Messaging.ServiceBus;
using Feedback.APIs;
using MassTransit;
using Nutrition.Library;

var builder = WebApplication.CreateBuilder(args);
var configurationBuilder = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();

var appSettings = configurationBuilder.Build().Get<AppSettings>()!;
builder.Services.AddSingleton(appSettings);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IFeedbackService, FeedbackService>();

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<NutritionCreatedConsumer>();
    x.SetKebabCaseEndpointNameFormatter();
    x.UsingAzureServiceBus((context, cfg) =>
    {
        cfg.Host(appSettings.AzureServiceBusConfiguration.ConnectionString, h =>
        {
            h.TransportType = ServiceBusTransportType.AmqpWebSockets;
        });
        cfg.Message<NutritionCreatedEvent>(m => m.SetEntityName("NutritionCreatedTopic"));
        cfg.SubscriptionEndpoint<NutritionCreatedEvent>("NutritionCreatedSubscription", configurator =>
        {
            configurator.ConfigureConsumer<NutritionCreatedConsumer>(context);
        });
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

{
    await using var scope = app.Services.CreateAsyncScope();
    await ((CosmoService<Feedback.APIs.Feedback>) scope.ServiceProvider.GetRequiredService<IFeedbackService>()).EnsureContainerExisted();
}

app.UseHttpsRedirection();
app.UseFeedbackEndpoints();

app.Run();
