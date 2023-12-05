using DnDProbabilityCalculator.Application.Table.Presentation;
using DnDProbabilityCalculator.Core.Adventuring;

namespace DnDProbabilityCalculator.Application.Table.Context;

public class ReceiveHitTable
{
    public required IEnumerable<string> AttackModifiers { get; init; }
    public required List<IEnumerable<string>> Probabilities { get; init; }

    private ReceiveHitTable()
    {
    }

    public static ReceiveHitTable FromActor(Actor actor, InputVariables inputVariables)
    {
        var attackModifierRow = new List<string> { $"{inputVariables.NumberOfAttacks} Attacks/Mod" }.Concat(inputVariables.AttackModifiers.Select(modifier => modifier.ToString())).ToList();

        var probabilityRows = Enumerable.Range(1, inputVariables.NumberOfAttacks)
            .Select(currentNumberOfHits => CreateGetHitRow(actor, inputVariables, currentNumberOfHits))
            .ToList();

        return new()
        {
            AttackModifiers = attackModifierRow,
            Probabilities = probabilityRows
        };
    }

    private static IEnumerable<string> CreateGetHitRow(Actor actor, InputVariables inputVariables, int currentNumberOfHits)
        => new[] { $"{currentNumberOfHits} Hits" }.Concat(inputVariables.AttackModifiers
            .Select(currentModifier =>
            {
                var probability = actor.ReceiveHitChance(currentModifier, inputVariables.NumberOfAttacks, currentNumberOfHits, inputVariables.Advantage).Probability;
                var successChance = ColoredSuccessChance.FromProbability(probability);
                return successChance.WithInvertedColors().ToString();
            }));
}