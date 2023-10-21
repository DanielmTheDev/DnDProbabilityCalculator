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
        var getHitRows = new List<string> { "1" }.Concat(attackModifiers.Select(modifier => ((SuccessChanceViewModel)actor.GetHitChance(modifier)).ToString()));
        return new()
        {
            AttackModifiers = attackModifierRow.ToList(),
            Probabilities = new() { getHitRows }
        };
    }
}