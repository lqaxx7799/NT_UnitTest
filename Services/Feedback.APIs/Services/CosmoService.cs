using Microsoft.Azure.Cosmos;

namespace Feedback.APIs;

public class CosmoService<T>
{
    protected readonly IConfiguration _configuration;
    protected CosmosClient _cosmosClient;
    protected Database _database;
    protected Container _container;

    public CosmoService(IConfiguration configuration)
    {
        _configuration = configuration;
        var endpointUri = configuration.GetSection("CosmoDBConfiguration").GetValue<string>("EndpointUri");
        var primaryKey = configuration.GetSection("CosmoDBConfiguration").GetValue<string>("PrimaryKey");
        var databaseName = configuration.GetSection("CosmoDBConfiguration").GetValue<string>("Database");

        _cosmosClient = new CosmosClient(endpointUri, primaryKey, new CosmosClientOptions{ ApplicationName = "Feedback.APIs" });
        _database = _cosmosClient.GetDatabase(databaseName);
        _container = _database.GetContainer(typeof(T).Name);
    }

    public async Task EnsureContainerExisted()
    {
        var databaseName = _configuration.GetSection("CosmoDBConfiguration").GetValue<string>("Database");
        Database database = await _cosmosClient.CreateDatabaseIfNotExistsAsync(databaseName);
        await database.CreateContainerIfNotExistsAsync(typeof(T).Name, "/partitionKey");
    }
}
