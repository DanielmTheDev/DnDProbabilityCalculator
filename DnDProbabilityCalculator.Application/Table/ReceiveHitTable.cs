using DnDProbabilityCalculator.Core.Adventuring;

namespace DnDProbabilityCalculator.Application.Table;

public class ReceiveHitTable
{
    public required IEnumerable<string> AttackModifiers { get; init; }
    public required List<IEnumerable<string>> Probabilities { get; init; }

    private ReceiveHitTable()
    {
    }

    public static ReceiveHitTable FromActor(Actor actor, int[] attackModifiers, int totalNumberOfAttacks)
    {
        var attackModifierRow = new List<string> { $"{totalNumberOfAttacks} Attacks/Mod" }.Concat(attackModifiers.Select(modifier => modifier.ToString())).ToList();

        var probabilityRows = Enumerable.Range(1, totalNumberOfAttacks)
            .Select(currentNumberOfHits => CreateGetHitRow(actor, attackModifiers, totalNumberOfAttacks, currentNumberOfHits))
            .ToList();

        return new()
        {
            AttackModifiers = attackModifierRow,
            Probabilities = probabilityRows
        };
    }

    private static IEnumerable<string> CreateGetHitRow(Actor actor, IEnumerable<int> attackModifiers, int totalNumberOfAttacks, int currentNumberOfHits)
        => new[] { $"{currentNumberOfHits} Hits" }.Concat(attackModifiers
            .Select(currentModifier =>
            {
                var probability = actor.ReceiveHitChance(currentModifier, totalNumberOfAttacks, currentNumberOfHits).Probability;
                var successChance = ColoredSuccessChance.FromProbability(probability);
                return successChance.WithInvertedColors().ToString();
            }));
}