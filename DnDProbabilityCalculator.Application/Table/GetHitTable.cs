using DnDProbabilityCalculator.Core.Adventuring;

namespace DnDProbabilityCalculator.Application.Table;

public class GetHitTable
{
    public required List<string> AttackModifiers { get; init; }
    public required List<IEnumerable<string>> Probabilities { get; init; }

    private GetHitTable()
    {
    }

    public static GetHitTable FromActor(Actor actor, int[] attackModifiers, int totalNumberOfAttacks)
    {
        var attackModifierRow = new List<string> { $"{totalNumberOfAttacks} Attacks/Mod" }.Concat(attackModifiers.Select(modifier => modifier.ToString()));

        var rows = Enumerable.Range(0, totalNumberOfAttacks + 1)
            .Select(currentNumberOfHits => CreateGetHitRow(actor, attackModifiers, totalNumberOfAttacks, currentNumberOfHits))
            .ToList();

        return new()
        {
            AttackModifiers = attackModifierRow.ToList(),
            Probabilities = rows
        };
    }

    private static IEnumerable<string> CreateGetHitRow(Actor actor, IEnumerable<int> attackModifiers, int totalNumberOfAttacks, int currentNumberOfHits)
        => new[] { $"{currentNumberOfHits} Hits" }.Concat(attackModifiers
            .Select(currentModifier =>
            {
                var probability = actor.ReceiveHitChance(currentModifier, totalNumberOfAttacks, currentNumberOfHits).Probability;
                var successChance = ColoredSuccessChance.FromProbability(probability);
                return currentNumberOfHits == 0
                    ? successChance.ToString()
                    : successChance.WithInvertedColors().ToString();
            }));
}