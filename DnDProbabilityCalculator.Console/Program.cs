using DnDProbabilityCalculator.Infrastructure.Actors;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

var services = new ServiceCollection();
AddConfiguration(services);

using var serviceProvider = services.BuildServiceProvider();
var mySettings = serviceProvider.GetService<IOptions<FileRepositoryOptions>>()!.Value;

// ReSharper disable once LocalizableElement
Console.WriteLine($"Setting1: {mySettings.FilePath}");

return;

void AddConfiguration(ServiceCollection serviceCollection)
{
    var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
    var configuration = builder.Build();
    serviceCollection.Configure<FileRepositoryOptions>(configuration.GetSection("FileRepository"));
}
// var party = Party.FromJsonString(jsonString);
// party.Dump();