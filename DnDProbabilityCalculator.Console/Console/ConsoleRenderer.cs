using DnDProbabilityCalculator.Application.Probabilities;
using Spectre.Console;

namespace DnDProbabilityCalculator.Console.Console;

public class ConsoleRenderer : IConsoleRenderer
{
    private readonly ITableContextService _tableContextService;

    public ConsoleRenderer(ITableContextService tableContextService)
    {
        _tableContextService = tableContextService;
    }

    public void Start()
    {
        var dcs = Enumerable.Range(9, 7).ToArray();
        var attackModifiers = Enumerable.Range(-1, 7).ToArray();
        var numberOfAttacks = 2;

        var tableContext = _tableContextService.Get(dcs, attackModifiers, numberOfAttacks);
        var table = new Table();
        tableContext.ForEach(data => table.AddColumn(data.ActorName));

        AnsiConsole.Live(table)
            .Start(context =>
            {
                table.RerenderRows(tableContext, context);

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
                            var newTableContext = _tableContextService.Get(dcs, attackModifiers, numberOfAttacks);
                            table.RerenderRows(newTableContext, context);
                            break;
                        case ConsoleKey.UpArrow:
                            numberOfAttacks++;
                            newTableContext = _tableContextService.Get(dcs, attackModifiers, numberOfAttacks);
                            table.RerenderRows(newTableContext, context);
                            break;
                        case ConsoleKey.Q:
                            quit = true;
                            break;
                    }
                }
            });
    }
}