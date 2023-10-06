using DnDProbabilityCalculator.Console.Composition;
using DnDProbabilityCalculator.Infrastructure.Actors;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

var services = new ServiceCollection();
services.AddConfiguration();

using var serviceProvider = services.BuildServiceProvider();
var mySettings = serviceProvider.GetService<IOptions<FileRepositoryOptions>>()!.Value;

// ReSharper disable once LocalizableElement
Console.WriteLine($"Setting1: {mySettings.FilePath}");

return;

// var party = Party.FromJsonString(jsonString);
// party.Dump();