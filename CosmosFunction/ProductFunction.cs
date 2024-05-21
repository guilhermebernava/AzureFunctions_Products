using CosmosFunction.Entities;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;

namespace CosmosFunction;

public class ProductFunction
{

    private readonly CosmosDbRepository _cosmosDbRepository;
    private readonly ILogger _logger;

    public ProductFunction(CosmosDbRepository cosmosDbRepository, ILoggerFactory loggerFactory)
    {
        _cosmosDbRepository = cosmosDbRepository;
        _logger = loggerFactory.CreateLogger<ProductFunction>();
    }

    [Function("CreateProduct")]
    public async Task<HttpResponseData> CreateProduct([HttpTrigger(AuthorizationLevel.Function, "post", Route = "product")] HttpRequestData req)
    {
        _logger.LogInformation("Creating a new product.");
        var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        var product = JsonConvert.DeserializeObject<Product>(requestBody);
        await _cosmosDbRepository.AddItemAsync(product);

        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteAsJsonAsync(product);
        return response;
    }

    [Function("GetProduct")]
    public async Task<HttpResponseData> GetProduct([HttpTrigger(AuthorizationLevel.Function, "get", Route = "product/{id}")] HttpRequestData req, string id)
    {
        _logger.LogInformation("Getting product item by id.");
        var product = await _cosmosDbRepository.GetItemAsync(id);
        if (product == null)
        {
            return req.CreateResponse(HttpStatusCode.NotFound);
        }

        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteAsJsonAsync(product);
        return response;
    }

    [Function("UpdateProduct")]
    public async Task<HttpResponseData> UpdateProduct([HttpTrigger(AuthorizationLevel.Function, "put", Route = "product/{id}")] HttpRequestData req, string id)
    {
        _logger.LogInformation("Updating a product item.");
        var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        var updatedProduct = JsonConvert.DeserializeObject<Product>(requestBody);
        await _cosmosDbRepository.UpdateItemAsync(id, updatedProduct);

        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteAsJsonAsync(updatedProduct);
        return response;
    }

    [Function("DeleteProduct")]
    public async Task<HttpResponseData> DeleteProduct([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "product/{id}")] HttpRequestData req, string id)
    {
        _logger.LogInformation("Deleting a product item.");
        await _cosmosDbRepository.DeleteItemAsync(id);

        var response = req.CreateResponse(HttpStatusCode.OK);
        return response;
    }
}
