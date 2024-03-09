using System.Text.Json;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices((context, services) =>
    {
        services.AddOptions<JsonSerializerOptions>()
            .Configure(options => { options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase; });

        services.AddSingleton(_ =>
        {
            var options = new CosmosClientOptions
            {
                SerializerOptions = new() { PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase }
            };
            return new CosmosClient(Environment.GetEnvironmentVariable("CosmosDbConnection"), options);
        });

        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(context.Configuration)
            .Enrich.FromLogContext()
            .CreateLogger();

        services.AddLogging(lb => lb.AddSerilog());
    })
    .Build();

host.Run();