using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureAppConfiguration((context, builder) =>
    {
        builder.AddEnvironmentVariables();
        if (context.HostingEnvironment.IsDevelopment())
        {
            builder.AddJsonFile("local.settings.json", optional: true, reloadOnChange: true);
        }
    })
    .ConfigureServices((context, services) =>
    {
        var configuration = context.Configuration;

        string accountEndpoint = configuration["CosmosDBEndpoint"];
        string accountKey = configuration["CosmosDBKey"];
        string databaseName = configuration["CosmosDBDatabaseName"];
        string containerName = configuration["CosmosDBContainerName"];

        if (accountEndpoint == null || accountKey == null || databaseName == null || containerName == null) throw new Exception("not found parameters");

        var client = new CosmosClient(accountEndpoint, accountKey);
        services.AddSingleton(new CosmosDbRepository(client, databaseName, containerName));
    })
    .Build();



host.Run();
