using DnDProbabilityCalculator.Core.Adventuring;

namespace DnDProbabilityCalculator.Application.Table.Context;

public class GeneralActorInfo
{
    public required string ActorName { get; init; }
    public required int ArmorClass { get; init; }
    public required double DamagePerHit { get; init; }

    public static GeneralActorInfo FromActor(Actor actor)
        => new() { ActorName = actor.Name, ArmorClass = actor.ArmorClass, DamagePerHit = actor.AverageDamagePerHit };
}