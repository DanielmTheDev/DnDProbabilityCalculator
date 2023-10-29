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

        // var rows = Enumerable.Range(1, totalNumberOfAttacks)
        //     .Select(numberOfHits => new []{$"{numberOfHits} Hits"}.Concat(attackModifiers
        //         .Select(attackModifier =>
        //         {
        //             var probabilities = actor.GetHitProbability(attackModifier, totalNumberOfAttacks, numberOfHits); // TODO: maybe change interface to also get changeForNumberOfHits => easier call
        //             return probabilities.Probabilities.Single(p => p.NumberOfHits == numberOfHits).Probability.ToString("P0");
        //         }))).ToList();

        return new()
        {
            AttackModifiers = attackModifierRow.ToList(),
            Probabilities = null
        };
    }
}