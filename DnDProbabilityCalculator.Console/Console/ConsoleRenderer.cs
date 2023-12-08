using DnDProbabilityCalculator.Application.Table;
using DnDProbabilityCalculator.Application.Table.Context;
using DnDProbabilityCalculator.Core.Adventuring;
using Spectre.Console;

namespace DnDProbabilityCalculator.Console.Console;

public class ConsoleRenderer(ITableContextFactory tableContextFactory) : IConsoleRenderer
{
    public void Start()
    {
        var inputVariables = CreateDefaultInputVariables();

        var tableContext = tableContextFactory.Create(inputVariables);
        var table = new Table { Expand = true };
        tableContext.ForEach(data => table.AddColumn(data.GeneralTableInfo.ActorName));
        AnsiConsole.Clear();
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
                            Rerender(inputVariables, table, context);
                            break;
                        case ConsoleKey.UpArrow:
                            if (inputVariables.NumberOfAttacks == 1)
                                break;
                            inputVariables = inputVariables.WithDecrementedNumberOfAttacks();
                            Rerender(inputVariables, table, context);
                            break;
                        case ConsoleKey.RightArrow:
                            inputVariables = inputVariables.WithIncrementedColumns();
                            Rerender(inputVariables, table, context);
                            break;
                        case ConsoleKey.LeftArrow:
                            inputVariables = inputVariables.WithDecrementedColumns();
                            Rerender(inputVariables, table, context);
                            break;
                        case ConsoleKey.A:
                            inputVariables = inputVariables.WithAdvantage();
                            Rerender(inputVariables, table, context);
                            break;
                        case ConsoleKey.D:
                            inputVariables = inputVariables.WithDisadvantage();
                            Rerender(inputVariables, table, context);
                            break;
                        case ConsoleKey.S:
                            inputVariables = inputVariables.WithNoAdvantage();
                            Rerender(inputVariables, table, context);
                            break;
                        case ConsoleKey.Q:
                            quit = true;
                            break;
                    }
                }
            });
    }

    private void Rerender(InputVariables inputVariables, Table table, LiveDisplayContext context)
    {
        var newTableContext = tableContextFactory.Create(inputVariables);
        table.RerenderRows(newTableContext, context);
    }

    private static InputVariables CreateDefaultInputVariables()
        => new(Enumerable.Range(12, 7).ToArray(), Enumerable.Range(3, 7).ToArray(), Enumerable.Range(10, 7).ToArray(), 2, AdvantageType.None);
}