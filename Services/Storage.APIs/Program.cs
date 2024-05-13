using Azure.Storage.Blobs;
using Storage.APIs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// builder.Services.AddAntiforgery();

var blobConnectionString = builder.Configuration.GetSection("AzureStorageConfiguration").GetValue<string>("ConnectionString");
var blobContainerName = builder.Configuration.GetSection("AzureStorageConfiguration").GetValue<string>("ContainerName");
builder.Services.AddSingleton(new BlobContainerClient(blobConnectionString, blobContainerName));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// app.UseAntiforgery();
app.UseStorageEndpoints();
app.UseHttpsRedirection();

app.Run();
