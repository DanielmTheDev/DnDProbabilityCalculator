using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices((context, services) =>
    {
        services.AddOptions<JsonSerializerOptions>()
            .Configure(options =>
            {
                options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            });

        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(context.Configuration)
            .Enrich.FromLogContext()
            .CreateLogger();

        services.AddLogging(lb => lb.AddSerilog());
    })
    .Build();

host.Run();