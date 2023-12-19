using DnDProbabilityCalculator.Core.Adventuring;

namespace DnDProbabilityCalculator.Application.Table.GeneralInfo;

public class GeneralTableInfo
{
    public required string ActorName { get; init; }
    public required int ArmorClass { get; init; }
    public required double DamagePerHit { get; init; }
    public required AdvantageType Advantage { get; init; }
    public required int AttackModifier { get; set; }

    public static GeneralTableInfo FromActor(Actor actor, InputVariables inputVariables)
        => new()
        {
            ActorName = actor.Name,
            AttackModifier = actor.AttackModifier,
            ArmorClass = actor.ArmorClass,
            DamagePerHit = actor.AverageDamagePerHit,
            Advantage = inputVariables.Advantage
        };
}