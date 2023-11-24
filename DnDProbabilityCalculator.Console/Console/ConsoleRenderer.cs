using DnDProbabilityCalculator.Application.Table;
using DnDProbabilityCalculator.Application.Table.Context;
using Spectre.Console;

namespace DnDProbabilityCalculator.Console.Console;

public class ConsoleRenderer : IConsoleRenderer
{
    private readonly ITableContextFactory _tableContextFactory;

    public ConsoleRenderer(ITableContextFactory tableContextFactory)
    {
        _tableContextFactory = tableContextFactory;
    }

    public void Start()
    {
        var inputVariables = CreateDefaultInputVariables();

        var tableContext = _tableContextFactory.Create(inputVariables);
        var table = new Table();
        tableContext.ForEach(data => table.AddColumn(data.GeneralActorInfo.ActorName));

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
                            var newTableContext = _tableContextFactory.Create(inputVariables);
                            table.RerenderRows(newTableContext, context);
                            break;
                        case ConsoleKey.UpArrow:
                            if (inputVariables.NumberOfAttacks == 1)
                                break;
                            inputVariables = inputVariables.WithDecrementedNumberOfAttacks();
                            newTableContext = _tableContextFactory.Create(inputVariables);
                            table.RerenderRows(newTableContext, context);
                            break;
                        case ConsoleKey.RightArrow:
                            inputVariables = inputVariables.WithIncrementedColumns();
                            newTableContext = _tableContextFactory.Create(inputVariables);
                            table.RerenderRows(newTableContext, context);
                            break;
                        case ConsoleKey.LeftArrow:
                            inputVariables = inputVariables.WithDecrementedColumns();
                            newTableContext = _tableContextFactory.Create(inputVariables);
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
        => new(Enumerable.Range(9, 7).ToArray(), Enumerable.Range(-1, 7).ToArray(), Enumerable.Range(10, 7).ToArray(), numberOfAttacks: 2);
}