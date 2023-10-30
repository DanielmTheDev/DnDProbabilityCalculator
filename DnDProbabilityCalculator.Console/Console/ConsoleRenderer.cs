using DnDProbabilityCalculator.Application.Probabilities;
using Spectre.Console;

namespace DnDProbabilityCalculator.Console.Console;

public class ConsoleRenderer : IConsoleRenderer
{
    private readonly ITableContextService _tableContextContextService;

    public ConsoleRenderer(ITableContextService tableContextContextService)
    {
        _tableContextContextService = tableContextContextService;
    }

    public void Start()
    {
        var dcs = Enumerable.Range(9, 7).ToArray();
        var attackModifiers = Enumerable.Range(-1, 7).ToArray();
        var numberOfAttacks = 2;
        var tableContext = _tableContextContextService.Get(dcs, attackModifiers, numberOfAttacks); // todo maybe distribute into two separate methods
        var completeTable = new Table();
        tableContext.ForEach(data => completeTable.AddColumn(data.ActorName));

        AnsiConsole.Live(completeTable)
            .Start(context =>
            {
                RerenderRows(completeTable, tableContext, context);
                context.Refresh();
                var quit = false;
                while (!quit)
                {
                    var key = System.Console.ReadKey(true);
                    switch (key.Key)
                    {
                        case ConsoleKey.DownArrow:
                            if (numberOfAttacks == 1)
                                break;
                            numberOfAttacks--;
                            var newTableContext = _tableContextContextService.Get(dcs, attackModifiers, numberOfAttacks);
                            RerenderRows(completeTable, newTableContext, context);
                            break;
                        case ConsoleKey.UpArrow:
                            numberOfAttacks++;
                            newTableContext = _tableContextContextService.Get(dcs, attackModifiers, numberOfAttacks);
                            RerenderRows(completeTable, newTableContext, context);
                            break;
                        case ConsoleKey.Q:
                            quit = true;
                            break;
                    }
                }
            });
    }

    private static void RerenderRows(Table theTable, List<ProbabilityTable> tableContext, LiveDisplayContext context)
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