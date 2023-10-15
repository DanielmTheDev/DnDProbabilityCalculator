using DnDProbabilityCalculator.Core.Adventuring.Abilities;

namespace DnDProbabilityCalculator.Core.Adventuring.ActorBuilder;

public class StrengthStage : IStrengthStage
{
    private readonly Actor _actor;

    public StrengthStage(Actor actor)
        => _actor = actor;

    public IDexterityStage WithStrength(int value, bool isProficient = false)
    {
        _actor.AbilityScores.Strength = new Strength { Value = value, IsProficient = isProficient };
        return new DexterityStage(_actor);
    }
}