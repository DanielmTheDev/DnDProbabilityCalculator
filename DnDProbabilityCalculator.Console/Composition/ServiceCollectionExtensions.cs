using DnDProbabilityCalculator.Infrastructure.Actors;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DnDProbabilityCalculator.Console.Composition;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddConfiguration(this IServiceCollection serviceCollection)
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        var configuration = builder.Build();
        serviceCollection.Configure<FileRepositoryOptions>(configuration.GetSection("FileRepository"));
        return serviceCollection;
    }
}