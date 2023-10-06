using DnDProbabilityCalculator.Application.Probabilities;
using DnDProbabilityCalculator.Console.Composition;
using Dumpify;
using Microsoft.Extensions.DependencyInjection;

var serviceProvider = new ServiceCollection()
    .AddConfiguration()
    .RegisterServices()
    .BuildServiceProvider();

var partyService = serviceProvider.GetService<IProbabilityTableService>()!;

var party = partyService.Get(10, 12, 14);
party.Dump();