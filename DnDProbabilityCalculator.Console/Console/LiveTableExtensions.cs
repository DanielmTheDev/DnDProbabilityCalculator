using DnDProbabilityCalculator.Application.Probabilities;
using Spectre.Console;

namespace DnDProbabilityCalculator.Console.Console;

public static class LiveTableExtensions
{
    public static void RerenderRows(this Table theTable, List<ProbabilityTable> tableContext, LiveDisplayContext context)
    {
        theTable.Rows.Clear();
        theTable.AddRow(CreateSavingThrowTables(tableContext));
        theTable.AddRow(CreateGetHitTables(tableContext));
        context.Refresh();
    }

    private static IEnumerable<Table> CreateGetHitTables(IEnumerable<ProbabilityTable> allActorTables)
        => allActorTables.Select(actorTable =>
        {
            var table = new Table();
            table.AddColumns(actorTable.GetHitTable.AttackModifiers.ToArray());
            actorTable.GetHitTable.Probabilities.ForEach(row => table.AddRow(row.ToArray()));
            return table;
        }).ToList();

    private static IEnumerable<Table> CreateSavingThrowTables(IEnumerable<ProbabilityTable> allActorTables)
        => allActorTables.Select(actorTable =>
        {
            var table = new Table();
            table.AddColumns(actorTable.SavingThrowTable.Dcs.ToArray());
            actorTable.SavingThrowTable.Probabilities.ForEach(row => table.AddRow(row.ToArray()));
            return table;
        }).ToList();
}