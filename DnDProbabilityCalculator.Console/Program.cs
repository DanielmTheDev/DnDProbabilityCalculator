using DnDProbabilityCalculator.Application.Probabilities;
using DnDProbabilityCalculator.Console.Composition;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;

var serviceProvider = new ServiceCollection()
    .AddConfiguration()
    .RegisterServices()
    .BuildServiceProvider();

var probabilityService = serviceProvider.GetService<IProbabilityTableService>()!;

var dcs = new[] { 10, 12, 14 };

var probabilityTables = probabilityService.Get(dcs);

// probabilityTables.ToList().ForEach(tableData =>
// {
//     var table = new Table();
//     tableData.DcProbabilities.ToList().ForEach(dcData =>
//     {
//           table.AddColumn(new(dcData.DC.ToString()));
//     });
//
//     AnsiConsole.Write(table);
// });