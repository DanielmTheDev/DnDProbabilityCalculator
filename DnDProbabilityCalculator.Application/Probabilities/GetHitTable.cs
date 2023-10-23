using DnDProbabilityCalculator.Core.Adventuring;

namespace DnDProbabilityCalculator.Application.Probabilities;

public class GetHitTable
{
    public required List<string> AttackModifiers { get; set; }
    public required List<IEnumerable<string>> Probabilities{ get; init; }

    private GetHitTable() { }

    public static GetHitTable FromActor(Actor actor, int[] attackModifiers)
    {
        var attackModifierRow = new List<string> { "#Attacks/Modifier" }.Concat(attackModifiers.Select(modifier => modifier.ToString()));

        var attackRow = attackModifiers.Select(attackModifier => actor.CalculateGetHitProbabilities(attackModifier, 5)).ToList();
        return new()
        {
            AttackModifiers = attackModifierRow.ToList(),
            Probabilities = null
        };
    }
}