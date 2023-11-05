using DnDProbabilityCalculator.Core.Adventuring.Abilities;

namespace DnDProbabilityCalculator.Core.Adventuring.ActorBuilder;

public class IntelligenceStage : IIntelligenceStage
{
    private readonly Actor _actor;

    public IntelligenceStage(Actor actor)
        => _actor = actor;

    public ICharismaStage WithIntelligence(int value, bool isProficient = false, bool isAttackAbility = false)
    {
        _actor.AbilityScores.Intelligence = new Intelligence { Value = value, IsProficient = isProficient, IsAttackAbility = isAttackAbility };
        return new CharismaStage(_actor);
    }
}