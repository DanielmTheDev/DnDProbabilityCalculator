using System.Globalization;
using DnDProbabilityCalculator.Application.Table.Context;
using Spectre.Console;

namespace DnDProbabilityCalculator.Console.Console;

public static class LiveTableExtensions
{
    public static void RerenderRows(this Table theTable, List<TableContext> allTableContexts, LiveDisplayContext context)
    {
        theTable.Rows.Clear();
        theTable.AddRow(CreateGeneralInformatioRow(allTableContexts));
        theTable.AddRow(CreateSavingThrowTables(allTableContexts));
        theTable.AddRow(CreateReceiveHitTables(allTableContexts));
        theTable.AddRow(CreateDeliverHitTables(allTableContexts));
        context.Refresh();
    }

    private static IEnumerable<Table> CreateGeneralInformatioRow(IEnumerable<TableContext> allTableContexts)
        => allTableContexts.Select(tableContext =>
        {
            var table = new Table
            {
                Title = new("General Information")
            };
            table.Expand();
            table.AddColumns("Armor Class", "Damage per Hit", "Advantage Type");
            table.AddRow(
                tableContext.GeneralTableInfo.ArmorClass.ToString(),
                tableContext.GeneralTableInfo.DamagePerHit.ToString(CultureInfo.CurrentCulture),
                tableContext.GeneralTableInfo.AdvantageType);
            return table;
        });

    private static IEnumerable<Table> CreateSavingThrowTables(IEnumerable<TableContext> allTableContexts)
        => allTableContexts.Select(tableContext =>
        {
            var table = new Table
            {
                Title = new("Saving Throws")
            };
            table.Expand();
            table.AddColumns(tableContext.SavingThrowTable.Dcs.ToArray());
            tableContext.SavingThrowTable.Probabilities.ForEach(row => table.AddRow(row.ToArray()));
            return table;
        });

    private static IEnumerable<Table> CreateReceiveHitTables(IEnumerable<TableContext> allTableContexts)
        => allTableContexts.Select(tableContext =>
        {

            var table = new Table
            {
                Title = new("Receive Hit")
            };
            table.Expand();
            table.AddColumns(tableContext.ReceiveHitTable.AttackModifiers.ToArray());
            tableContext.ReceiveHitTable.Probabilities.ForEach(row => table.AddRow(row.ToArray()));
            return table;
        });

    private static IEnumerable<Table> CreateDeliverHitTables(IEnumerable<TableContext> allTableContexts)
        => allTableContexts.Select(tableContext =>
        {
            var table = new Table
            {
                Title = new("Deliver Hit")
            };
            table.Expand();
            table.AddColumns(tableContext.DeliverHitTable.ArmorClasses.ToArray());
            tableContext.DeliverHitTable.Probabilities.ForEach(row => table.AddRow(row.ToArray()));
            return table;
        });
}