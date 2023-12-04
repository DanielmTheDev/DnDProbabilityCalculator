using DnDProbabilityCalculator.Core.Adventuring;

namespace DnDProbabilityCalculator.Application.Table.Context;

public class GeneralTableInfo
{
    public required string ActorName { get; init; }
    public required int ArmorClass { get; init; }
    public required double DamagePerHit { get; init; }
    public required string AdvantageType { get; init; }

    public static GeneralTableInfo FromActor(Actor actor, InputVariables inputVariables)
        => new()
        {
            ActorName = actor.Name,
            ArmorClass = actor.ArmorClass,
            DamagePerHit = actor.AverageDamagePerHit,
            AdvantageType = inputVariables.AdvantageType.ToString()
        };
}