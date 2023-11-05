using DnDProbabilityCalculator.Core.Adventuring.Abilities;

namespace DnDProbabilityCalculator.Core.Adventuring.ActorBuilder;

public class AttackAbilityStage : IAttackAbilityStage
{
    private readonly Actor _actor;

    public AttackAbilityStage(Actor actor)
        => _actor = actor;

    public IBuildStage WithAttackAbility(AbilityScoreType abilityScoreType)
    {
        _actor.AttackAbility = abilityScoreType;
        return new BuildStage(_actor);
    }
}