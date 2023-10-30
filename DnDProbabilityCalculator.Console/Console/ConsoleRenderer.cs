using DnDProbabilityCalculator.Application.Probabilities;
using Spectre.Console;

namespace DnDProbabilityCalculator.Console.Console;

public class ConsoleRenderer : IConsoleRenderer
{
    private readonly IProbabilityTableService _probabilityTableService;

    public ConsoleRenderer(IProbabilityTableService probabilityTableService)
    {
        _probabilityTableService = probabilityTableService;
    }

    public void Start()
    {
        var dcs = Enumerable.Range(9, 7).ToArray();
        var attackModifiers = Enumerable.Range(-1, 7).ToArray();
        var numberOfAttacks = 2;
        var allActorTables = _probabilityTableService.Get(dcs, attackModifiers, numberOfAttacks); // todo maybe distribute into two separate methods

        var savingThrowTables = CreateSavingThrowTables(allActorTables);

        var getHitTables = CreateGetHitTables(allActorTables);

        var completeTable = new Table();
        allActorTables.ForEach(data => completeTable.AddColumn(data.ActorName));
        completeTable.AddRow(savingThrowTables).AddRow(getHitTables);


        AnsiConsole.Live(completeTable)
            .Start(context =>
            {
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
                            allActorTables = _probabilityTableService.Get(dcs, attackModifiers, numberOfAttacks);
                            completeTable.RemoveRow(completeTable.Rows.Count - 1);
                            completeTable.AddRow(CreateGetHitTables(allActorTables));
                            context.Refresh();
                            break;
                        case ConsoleKey.UpArrow:
                            numberOfAttacks++;
                            allActorTables = _probabilityTableService.Get(dcs, attackModifiers, numberOfAttacks);
                            completeTable.RemoveRow(completeTable.Rows.Count - 1);
                            completeTable.AddRow(CreateGetHitTables(allActorTables));
                            context.Refresh();
                            break;
                        case ConsoleKey.Q:
                            quit = true;
                            break;
                    }
                }
            });
    }

    IEnumerable<Table> CreateGetHitTables(List<ProbabilityTable> probabilityTables)
    {
        return probabilityTables.Select(actorTable =>
        {
            var table = new Table();
            table.AddColumns(actorTable.GetHitTable.AttackModifiers.ToArray());
            actorTable.GetHitTable.Probabilities.ForEach(row => table.AddRow(row.ToArray()));
            return table;
        }).ToList();
    }

    IEnumerable<Table> CreateSavingThrowTables(List<ProbabilityTable> list)
    {
        return list.Select(actorTable =>
        {
            var table = new Table();
            table.AddColumns(actorTable.SavingThrowTable.Dcs.ToArray());
            actorTable.SavingThrowTable.Probabilities.ForEach(row => table.AddRow(row.ToArray()));
            return table;
        }).ToList();
    }
}