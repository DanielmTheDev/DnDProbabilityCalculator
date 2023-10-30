using DnDProbabilityCalculator.Console.Composition;
using DnDProbabilityCalculator.Console.Console;
using Microsoft.Extensions.DependencyInjection;

var serviceProvider = new ServiceCollection()
    .AddConfiguration()
    .RegisterServices()
    .BuildServiceProvider();

var consoleRenderer = serviceProvider.GetService<IConsoleRenderer>()!;

consoleRenderer.Start();
