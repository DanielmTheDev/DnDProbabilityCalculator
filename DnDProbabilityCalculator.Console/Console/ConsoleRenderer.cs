using DnDProbabilityCalculator.Application.Table;
using DnDProbabilityCalculator.Core;
using DnDProbabilityCalculator.Core.Adventuring;
using Spectre.Console;

namespace DnDProbabilityCalculator.Console.Console;

public class ConsoleRenderer(ITableContextFactory tableContextFactory, IPartyRepository partyRepository) : IConsoleRenderer
{
    public void Start()
    {
        var inputVariables = InputVariables.CreateDefaultInputVariables();
        var party = partyRepository.Get();
        var tableContext = tableContextFactory.Create(inputVariables, party);
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
                            Rerender(inputVariables, party, table, context);
                            break;
                        case ConsoleKey.UpArrow:
                            if (inputVariables.NumberOfAttacks == 1)
                                break;
                            inputVariables = inputVariables.WithDecrementedNumberOfAttacks();
                            Rerender(inputVariables, party, table, context);
                            break;
                        case ConsoleKey.RightArrow:
                            inputVariables = inputVariables.WithIncrementedColumns();
                            Rerender(inputVariables, party, table, context);
                            break;
                        case ConsoleKey.LeftArrow:
                            inputVariables = inputVariables.WithDecrementedColumns();
                            Rerender(inputVariables, party, table, context);
                            break;
                        case ConsoleKey.A:
                            inputVariables = inputVariables.WithAdvantage();
                            Rerender(inputVariables, party, table, context);
                            break;
                        case ConsoleKey.D:
                            inputVariables = inputVariables.WithDisadvantage();
                            Rerender(inputVariables, party, table, context);
                            break;
                        case ConsoleKey.S:
                            inputVariables = inputVariables.WithNoAdvantage();
                            Rerender(inputVariables, party, table, context);
                            break;
                        case ConsoleKey.Q:
                            quit = true;
                            break;
                    }
                }
            });
    }

    private void Rerender(InputVariables inputVariables, Party party, Table table, LiveDisplayContext context)
    {
        var newTableContext = tableContextFactory.Create(inputVariables, party);
        table.RerenderRows(newTableContext, context);
    }
}