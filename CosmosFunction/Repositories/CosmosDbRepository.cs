using CosmosFunction.Entities;
using Microsoft.Azure.Cosmos;

public class CosmosDbRepository
{
    private Container _container;

    public CosmosDbRepository(CosmosClient dbClient, string databaseName, string containerName)
    {
        _container = dbClient.GetContainer(databaseName, containerName);
    }

    public async Task AddItemAsync(Product product)
    {
        try
        {
            await _container.CreateItemAsync(product, new PartitionKey(product.id));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task<Product> GetItemAsync(string id)
    {
        try
        {
            ItemResponse<Product> response = await _container.ReadItemAsync<Product>(id, new PartitionKey(id));
            return response.Resource;
        }
        catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }
    }

    public async Task<IEnumerable<Product>> GetItemsAsync(string queryString)
    {
        var query = _container.GetItemQueryIterator<Product>(new QueryDefinition(queryString));
        List<Product> results = new List<Product>();
        while (query.HasMoreResults)
        {
            var response = await query.ReadNextAsync();
            results.AddRange(response.ToList());
        }
        return results;
    }

    public async Task UpdateItemAsync(string id, Product product)
    {
        await _container.UpsertItemAsync(product, new PartitionKey(id));
    }

    public async Task DeleteItemAsync(string id)
    {
        await _container.DeleteItemAsync<Product>(id, new PartitionKey(id));
    }
}
