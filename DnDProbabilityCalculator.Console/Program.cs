using System.Globalization;
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

var allTableData = probabilityService.Get(dcs);

// allTableData.ForEach(tableData =>
// {
//     var table = new Table();
//     table.AddColumn(new(tableData.ActorName));
//     tableData.DCs.ToList().ForEach(dc => table.AddColumn(new($"DC {dc}")));
//     var row = tableData.StrengthProbabilities.Select(probability => probability.ToString(CultureInfo.InvariantCulture)).ToList();
//     row.Insert(0, "Strength");
//     table.AddRow(row.ToArray());
//     AnsiConsole.Write(table);
// });

