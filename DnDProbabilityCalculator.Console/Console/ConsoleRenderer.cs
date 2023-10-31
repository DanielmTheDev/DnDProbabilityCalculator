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
        var inputVariables = CreateDefaultInputVariables();

        var tableContext = _tableContextService.Get(inputVariables);
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
                            if (inputVariables.NumberOfAttacks == 1)
                                break;
                            inputVariables = inputVariables with { NumberOfAttacks = inputVariables.NumberOfAttacks - 1 };
                            var newTableContext = _tableContextService.Get(inputVariables);
                            table.RerenderRows(newTableContext, context);
                            break;
                        case ConsoleKey.UpArrow:
                            inputVariables = inputVariables with { NumberOfAttacks = inputVariables.NumberOfAttacks +1 };
                            newTableContext = _tableContextService.Get(inputVariables);
                            table.RerenderRows(newTableContext, context);
                            break;
                        case ConsoleKey.Q:
                            quit = true;
                            break;
                    }
                }
            });
    }

    private InputVariables CreateDefaultInputVariables()
        => new()
        {
            Dcs = Enumerable.Range(9, 7).ToArray(),
            AttackModifiers = Enumerable.Range(-1, 7).ToArray(),
            NumberOfAttacks = 2
        };
}