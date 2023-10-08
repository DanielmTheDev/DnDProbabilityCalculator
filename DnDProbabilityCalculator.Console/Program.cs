using DnDProbabilityCalculator.Application.Probabilities;
using DnDProbabilityCalculator.Console.Composition;
using Dumpify;
using Microsoft.Extensions.DependencyInjection;

var serviceProvider = new ServiceCollection()
    .AddConfiguration()
    .RegisterServices()
    .BuildServiceProvider();

var probabilityService = serviceProvider.GetService<IProbabilityTableService>()!;

var probabilityTable = probabilityService.Get(10, 12, 14);


probabilityTable.Dump();