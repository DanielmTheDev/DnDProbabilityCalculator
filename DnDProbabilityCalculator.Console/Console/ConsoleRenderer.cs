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
                            inputVariables = new(inputVariables.Dcs, inputVariables.AttackModifiers, inputVariables.NumberOfAttacks - 1);
                            var newTableContext = _tableContextService.Get(inputVariables);
                            table.RerenderRows(newTableContext, context);
                            break;
                        case ConsoleKey.UpArrow:
                            inputVariables = new(inputVariables.Dcs, inputVariables.AttackModifiers, inputVariables.NumberOfAttacks + 1);
                            newTableContext = _tableContextService.Get(inputVariables);
                            table.RerenderRows(newTableContext, context);
                            break;
                        case ConsoleKey.RightArrow:
                            var increasedDcs = inputVariables.Dcs.Skip(1).Concat(new[] { inputVariables.Dcs.Last() + 1 });
                            var increasedAttackModifiers = inputVariables.AttackModifiers.Skip(1).Concat(new[] { inputVariables.AttackModifiers.Last() + 1 });
                            inputVariables = new(increasedDcs.ToArray(), increasedAttackModifiers.ToArray(), inputVariables.NumberOfAttacks);
                            newTableContext = _tableContextService.Get(inputVariables);
                            table.RerenderRows(newTableContext, context);
                            break;
                        case ConsoleKey.LeftArrow:
                            var decreasedDcs = new[] { inputVariables.Dcs.Last() - 1 }.Concat(inputVariables.Dcs.Take(inputVariables.Dcs.Length - 1));
                            var decreasedAttackModifiers = new[] { inputVariables.AttackModifiers.Last() - 1 }.Concat(inputVariables.AttackModifiers.Take(inputVariables.AttackModifiers.Length - 1));
                            inputVariables = new(decreasedDcs.ToArray(), decreasedAttackModifiers.ToArray(), inputVariables.NumberOfAttacks);
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