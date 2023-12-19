using DnDProbabilityCalculator.Core.Adventuring;

namespace DnDProbabilityCalculator.Application.Table.GeneralInfo;

public class GeneralTableInfo
{
    public required string ActorName { get; init; }
    public required int ArmorClass { get; init; }
    public required double DamagePerHit { get; init; }
    public required string Advantage { get; init; }
    public required int AttackModifier { get; set; }

    public static GeneralTableInfo FromActor(Actor actor, InputVariables inputVariables)
        => new()
        {
            ActorName = actor.Name,
            AttackModifier = actor.AttackModifier,
            ArmorClass = actor.ArmorClass,
            DamagePerHit = actor.AverageDamagePerHit,
            Advantage = ColoredAdvantageType(inputVariables.Advantage)
        };

    private static string ColoredAdvantageType(AdvantageType advantage)
    {
        return advantage switch
        {
            AdvantageType.None => advantage.ToString(),
            AdvantageType.Advantage => advantage.ToString(),
            AdvantageType.Disadvantage => advantage.ToString(),
            _ => throw new ArgumentOutOfRangeException(nameof(advantage), advantage, null)
        };
    }
}