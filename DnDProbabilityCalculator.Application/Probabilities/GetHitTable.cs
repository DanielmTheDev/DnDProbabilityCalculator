using DnDProbabilityCalculator.Core.Adventuring;

namespace DnDProbabilityCalculator.Application.Probabilities;

public class GetHitTable
{
    public required List<string> AttackModifiers { get; set; }
    public required List<IEnumerable<string>> Probabilities { get; init; }

    private GetHitTable()
    {
    }

    public static GetHitTable FromActor(Actor actor, int[] attackModifiers)
    {
        var attackModifierRow = new List<string> { "#Attacks/Modifier" }.Concat(attackModifiers.Select(modifier => modifier.ToString()));
        var rows = Enumerable.Range(1, 3)
            .Select(numberOfAttacks => new List<string> { $"{numberOfAttacks}" }.Concat(attackModifiers
                .Select(attackModifier => actor.CalculateGetHitProbabilities(attackModifier, numberOfAttacks)
                    .Probabilities.Aggregate("", (cell, probability) => cell + $"hits: {probability.NumberOfHits}: {probability.Probability:P2}\n")))).ToList();

        return new()
        {
            AttackModifiers = attackModifierRow.ToList(),
            Probabilities = rows
        };
    }
}