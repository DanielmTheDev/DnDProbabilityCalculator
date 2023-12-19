using System.Globalization;
using DnDProbabilityCalculator.Application.Table;
using DnDProbabilityCalculator.Application.Table.Presentation;
using DnDProbabilityCalculator.Core.Adventuring.Abilities;
using Spectre.Console;

namespace DnDProbabilityCalculator.Console.Console;

public static class TableCreationExtensions
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
            table.AddColumns("Armor Class", "Attack Modifier", "Damage per Hit", "Advantage Type");
            table.AddRow(
                tableContext.GeneralTableInfo.ArmorClass.ToString(),
                tableContext.GeneralTableInfo.AttackModifier.ToString(),
                tableContext.GeneralTableInfo.DamagePerHit.ToString(CultureInfo.CurrentCulture),
                tableContext.GeneralTableInfo.Advantage);
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
            table.AddColumns(["Ability/DC", ..tableContext.SavingThrowTable.Dcs.Select(dc => dc.ToString())]);
            tableContext.SavingThrowTable.Probabilities
                .Select(row =>
                {
                    var abilityScoreCell = row.IsProficient
                        ? row.AbilityScoreType.Abbreviated().AsGreen()
                        : row.AbilityScoreType.Abbreviated();
                    abilityScoreCell = $"{abilityScoreCell} ({row.AbilityScore})";
                    return (List<string>) [abilityScoreCell, ..row.Cells.Select(probability => ColoredSuccessChance.FromProbability(probability).ToString())];
                })
                .ToList()
                .ForEach(row => table.AddRow(row.ToArray()));
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
            table.AddColumns([$"{tableContext.DeliverHitTable.TotalNumberOfAttacks} Attacks/AC", ..tableContext.DeliverHitTable.ArmorClasses.Select(ac => ac.ToString())]);
            tableContext.DeliverHitTable.Probabilities
                .Select(row => (List<string>) [$">= {row.NumberOfHits} Hits", ..row.Cells.Select(cell => ColoredSuccessChance.FromProbability(cell).ToString())])
                .ToList()
                .ForEach(row => table.AddRow(row.ToArray()));
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
            table.AddColumns([$"{tableContext.ReceiveHitTable.TotalNumberOfAttacks} Attacks/Mod", ..tableContext.ReceiveHitTable.AttackModifiers.Select(ac => ac.ToString())]);
            tableContext.ReceiveHitTable.Probabilities
                .Select(row => (List<string>) [$">= {row.NumberOfHits} Hits", ..row.Cells.Select(cell => ColoredSuccessChance.FromProbability(cell).WithInvertedColors().ToString())])
                .ToList()
                .ForEach(row => table.AddRow(row.ToArray()));
            return table;
        });
}