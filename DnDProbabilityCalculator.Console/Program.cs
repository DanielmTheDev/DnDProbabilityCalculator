using DnDProbabilityCalculator.Application.Probabilities;
using DnDProbabilityCalculator.Console.Composition;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;

var serviceProvider = new ServiceCollection()
    .AddConfiguration()
    .RegisterServices()
    .BuildServiceProvider();

var probabilityService = serviceProvider.GetService<IProbabilityTableService>()!;
var dcs = Enumerable.Range(9, 7).ToArray();
var attackModifiers = Enumerable.Range(9, 7).ToArray();
var allTableData = probabilityService.Get(dcs, attackModifiers);

var actorTables = allTableData.Select(tableData =>
{
    var table = new Table();
    table.AddColumns(tableData.DcRow.ToArray());
    tableData.SavingThrowRows.ForEach(row => table.AddRow(row.ToArray()));
    return table;
}).ToList();

var completeTable = new Table();
allTableData.ForEach(data => completeTable.AddColumn(data.Header));
AnsiConsole.Write(completeTable.AddRow(actorTables));