using DnDProbabilityCalculator.Core.Adventuring;

namespace DnDProbabilityCalculator.Application.Probabilities;

public class GetHitTable
{
    public required List<string> AttackModifiers { get; set; }
    public required List<IEnumerable<string>> Probabilities { get; init; }

    private GetHitTable()
    {
    }

    public static GetHitTable FromActor(Actor actor, int[] attackModifiers, int totalNumberOfAttacks)
    {
        var attackModifierRow = new List<string> { $"{totalNumberOfAttacks} Attacks/Mod" }.Concat(attackModifiers.Select(modifier => modifier.ToString()));
        var rows = Enumerable.Range(0, totalNumberOfAttacks + 1)
            .Select(currentNumberOfHits => new [] { $"{currentNumberOfHits} Hits"}.Concat(attackModifiers
                .Select(currentModifier => actor.GetHitProbability(currentModifier, totalNumberOfAttacks, currentNumberOfHits).ToString())))
            .ToList();

        return new()
        {
            AttackModifiers = attackModifierRow.ToList(),
            Probabilities = rows
        };
    }
}