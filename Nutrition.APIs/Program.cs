using Microsoft.EntityFrameworkCore;
using Nutrition.APIs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSqlServer<NutritionContext>(builder.Configuration.GetConnectionString("DefaultConnection"));
AddBusinessServices(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

await MigrateDatabase();
app.UseHttpsRedirection();

app.UseFoodEndpoints();
app.UseMealEndpoints();
app.UseNutritionEndpoints();
app.UseReportEndpoints();

app.Run();

void AddBusinessServices(IServiceCollection services)
{
    services.AddScoped<IFoodService, FoodService>();
    services.AddScoped<IMealService, MealService>();
    services.AddScoped<INutritionService, NutritionService>();
    services.AddScoped<IReportService, ReportService>();
}

async Task MigrateDatabase()
{
    await using var scope = app.Services.CreateAsyncScope();
    var context = scope.ServiceProvider.GetRequiredService<NutritionContext>();
    await context.Database.EnsureCreatedAsync();
    await context.Database.MigrateAsync();
    await NutritionSeedData.SeedData(context);
    await CategorySeedData.SeedData(context);
}
