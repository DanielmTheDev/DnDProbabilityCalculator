using DnDProbabilityCalculator.Application.Adventuring;
using DnDProbabilityCalculator.Console.Composition;
using Dumpify;
using Microsoft.Extensions.DependencyInjection;

var serviceProvider = new ServiceCollection()
    .AddConfiguration()
    .RegisterServices()
    .BuildServiceProvider();

var partyService = serviceProvider.GetService<IPartyService>()!;

var party = partyService.Get();
party.Dump();
