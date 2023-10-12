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

var actorTables = allTableData.Select(tableData =>
{
    var table = new Table();
    table.AddColumns(tableData.DcRow.ToArray());
    tableData.AllRows.ForEach(row => table.AddRow(row.ToArray()));
    return table;
}).ToList();

var completeTable = new Table();
allTableData.ForEach(data => completeTable.AddColumn(data.Header));
AnsiConsole.Write(completeTable.AddRow(actorTables));