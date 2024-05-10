using Feedback.APIs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IFeedbackService, FeedbackService>();

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
