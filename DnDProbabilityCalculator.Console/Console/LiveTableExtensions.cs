using DnDProbabilityCalculator.Application.Table;
using Spectre.Console;

namespace DnDProbabilityCalculator.Console.Console;

public static class LiveTableExtensions
{
    public static void RerenderRows(this Table theTable, List<TableContext> tableContext, LiveDisplayContext context)
    {
        theTable.Rows.Clear();
        theTable.AddRow(CreateSavingThrowTables(tableContext));
        theTable.AddRow(CreateReceiveHitTables(tableContext));
        context.Refresh();
    }

    private static IEnumerable<Table> CreateReceiveHitTables(IEnumerable<TableContext> allActorTables)
        => allActorTables.Select(actorTable =>
        {
            var table = new Table();
            table.AddColumns(actorTable.ReceiveHitTable.AttackModifiers.ToArray());
            actorTable.ReceiveHitTable.Probabilities.ForEach(row => table.AddRow(row.ToArray()));
            return table;
        }).ToList();

    private static IEnumerable<Table> CreateSavingThrowTables(IEnumerable<TableContext> allActorTables)
        => allActorTables.Select(actorTable =>
        {
            var table = new Table();
            table.AddColumns(actorTable.SavingThrowTable.Dcs.ToArray());
            actorTable.SavingThrowTable.Probabilities.ForEach(row => table.AddRow(row.ToArray()));
            return table;
        }).ToList();
}