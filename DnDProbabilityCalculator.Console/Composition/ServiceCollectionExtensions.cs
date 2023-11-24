using DnDProbabilityCalculator.Application.Table.Context;
using DnDProbabilityCalculator.Console.Console;
using DnDProbabilityCalculator.Core;
using DnDProbabilityCalculator.Infrastructure.Actors;
using DnDProbabilityCalculator.Infrastructure.FileSystem;
using DnDProbabilityCalculator.Infrastructure.Settings;
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
        return serviceCollection.Configure<FileRepositoryOptions>(configuration.GetSection(nameof(ApplicationSettings.FileRepository)));
    }

    public static IServiceCollection RegisterServices(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddScoped<IConsoleRenderer, ConsoleRenderer>()
            .AddScoped<ITableContextFactory, TableContextFactory>()
            .AddScoped<IPartyRepository, PartyFileRepository>()
            .AddScoped<IFileAccessor, FileAccessor>();
}