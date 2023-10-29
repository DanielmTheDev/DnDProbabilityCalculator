using DnDProbabilityCalculator.Application.Probabilities;
using DnDProbabilityCalculator.Console.Composition;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;

var serviceProvider = new ServiceCollection()
    .AddConfiguration()
    .RegisterServices()
    .BuildServiceProvider();

var probabilityTableService = serviceProvider.GetService<IProbabilityTableService>()!;
var dcs = Enumerable.Range(9, 7).ToArray();
var attackModifiers = Enumerable.Range(-1, 7).ToArray();
var allActorTables = probabilityTableService.Get(dcs, attackModifiers, 2);

var savingThrowTables = allActorTables.Select(actorTable =>
{
    var table = new Table();
    table.AddColumns(actorTable.SavingThrowTable.Dcs.ToArray());
    actorTable.SavingThrowTable.Probabilities.ForEach(row => table.AddRow(row.ToArray()));
    return table;
}).ToList();

var getHitTables = allActorTables.Select(actorTable =>
{
    var table = new Table();
    table.AddColumns(actorTable.GetHitTable.AttackModifiers.ToArray());
    actorTable.GetHitTable.Probabilities.ForEach(row => table.AddRow(row.ToArray()));
    return table;
}).ToList();

var completeTable = new Table();
allActorTables.ForEach(data => completeTable.AddColumn(data.ActorName));
AnsiConsole.Write(completeTable.AddRow(savingThrowTables).AddRow(getHitTables));