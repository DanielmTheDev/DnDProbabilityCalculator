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
                            inputVariables = inputVariables.WithIncrementedNumberOfAttacks();
                            var newTableContext = _tableContextService.Get(inputVariables);
                            table.RerenderRows(newTableContext, context);
                            break;
                        case ConsoleKey.UpArrow:
                            if (inputVariables.NumberOfAttacks == 1)
                                break;
                            inputVariables = inputVariables.WithDecrementedNumberOfAttacks();
                            newTableContext = _tableContextService.Get(inputVariables);
                            table.RerenderRows(newTableContext, context);
                            break;
                        case ConsoleKey.RightArrow:
                            inputVariables = inputVariables.WithIncrementedDcsAndModifiers();
                            newTableContext = _tableContextService.Get(inputVariables);
                            table.RerenderRows(newTableContext, context);
                            break;
                        case ConsoleKey.LeftArrow:
                            inputVariables = inputVariables.WithDecrementedDcsAndModifiers();
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
        => new(dcs: Enumerable.Range(9, 7).ToArray(), attackModifiers: Enumerable.Range(-1, 7).ToArray(), numberOfAttacks: 2);
}