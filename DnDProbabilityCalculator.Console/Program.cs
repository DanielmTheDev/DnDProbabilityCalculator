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

allTableData.ForEach(tableData =>
{
    var table = new Table();
    table.AddColumns(tableData.HeaderRow.ToArray());
    tableData.AllRows.ForEach(row => table.AddRow(row.ToArray()));
    AnsiConsole.Write(table);
});
