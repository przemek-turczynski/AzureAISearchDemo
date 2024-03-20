using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;
using Newtonsoft.Json;

namespace DataSeeder;

internal class Program
{
    private static readonly string ConnectionString = @"";

    static async Task Main(string[] args)
    {
        // read demo data
        string jsonHotelsData = File.ReadAllText("HotelsData.json");
        var hotelsData = JsonConvert.DeserializeObject<HotelsData>(jsonHotelsData);

        // create cosmos db connection
        using var cosmosClient = new CosmosClientBuilder(ConnectionString).Build();
        var cosmosDb = cosmosClient.GetDatabase("db");
        var container = cosmosDb.GetContainer("hotels");

        try
        {
            // recreate container to remove old data
            await container.DeleteContainerAsync();
        }
        catch(Exception) { }
        
        container = await cosmosDb.CreateContainerAsync(new ContainerProperties { Id = "hotels", PartitionKeyPath = "/id" });

        // save documents to cosmos db container
        foreach (var hotel in hotelsData!.Values)
        {
            await container!.UpsertItemAsync(hotel, new PartitionKey(hotel.Id));
        }
    }
}
