using DnDProbabilityCalculator.Core.Adventuring.Abilities;

namespace DnDProbabilityCalculator.Core.Adventuring.ActorBuilder;

public class ConstitutionStage : IConstitutionStage
{
    private readonly Actor _actor;

    public ConstitutionStage(Actor actor)
        => _actor = actor;

    public IWisdomStage WithConstitution(int value, bool isProficient = false, bool isAttackAbility = false)
    {
        _actor.AbilityScores.Constitution = new Constitution { Value = value, IsProficient = isProficient, IsAttackAbility = isAttackAbility };
        return new WisdomStage(_actor);
    }
}