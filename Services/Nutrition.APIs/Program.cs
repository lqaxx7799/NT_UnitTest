using Azure.Messaging.ServiceBus;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Nutrition.APIs;
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
builder.Services.AddCors(options =>
{
    options.AddPolicy("NutritionPolicy",
        builder =>
        {
            builder
                .WithOrigins("http://localhost:5173", "https://ashy-plant-07a8d8000.5.azurestaticapps.net")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});
builder.Services.AddSqlServer<NutritionContext>(appSettings.ConnectionStrings["DefaultConnection"]);
AddBusinessServices(builder.Services);

builder.Services.AddMassTransit(x =>
{
    x.SetKebabCaseEndpointNameFormatter();
    x.UsingAzureServiceBus((context, cfg) =>
    {
        cfg.Host(appSettings.AzureServiceBusConfiguration.ConnectionString, h =>
        {
            h.TransportType = ServiceBusTransportType.AmqpWebSockets;
        });
        cfg.Message<NutritionCreatedEvent>(m => m.SetEntityName("NutritionCreatedTopic"));
        // cfg.ReceiveEndpoint(appSettings.AzureServiceBusConfiguration.QueueName, e =>
        // {
        //     e.PrefetchCount = 100;

        //     // number of messages to deliver concurrently
        //     e.ConcurrentMessageLimit = 100;

        //     // default, but shown for example
        //     e.LockDuration = TimeSpan.FromMinutes(5);

        //     // lock will be renewed up to 30 minutes
        //     e.MaxAutoRenewDuration = TimeSpan.FromMinutes(30);
        // });
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("NutritionPolicy");

await MigrateDatabase();
app.UseHttpsRedirection();

app.UseCategoryEndpoints();
app.UseFoodEndpoints();
app.UseMealEndpoints();
app.UseNutritionEndpoints();
app.UseReportEndpoints();

app.Run();

void AddBusinessServices(IServiceCollection services)
{
    services.AddScoped<ICategoryService, CategoryService>();
    services.AddScoped<IFoodService, FoodService>();
    services.AddScoped<IMealService, MealService>();
    services.AddScoped<INutritionService, NutritionService>();
    services.AddScoped<IReportService, ReportService>();
}

async Task MigrateDatabase()
{
    await using var scope = app.Services.CreateAsyncScope();
    var context = scope.ServiceProvider.GetRequiredService<NutritionContext>();
    // await context.Database.EnsureCreatedAsync();
    await context.Database.MigrateAsync();
    await NutritionSeedData.SeedData(context);
    await CategorySeedData.SeedData(context);
    await MealSeedData.SeedData(context);
}
