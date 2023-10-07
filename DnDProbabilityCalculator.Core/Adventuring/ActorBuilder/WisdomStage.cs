namespace DnDProbabilityCalculator.Core.Adventuring.ActorBuilder;

public class WisdomStage : IWisdomStage
{
    private readonly Actor _actor;

    public WisdomStage(Actor actor)
        => _actor = actor;

    public IIntelligenceStage WithWisdom(int value, bool isProficient = false)
    {
        _actor.AbilityScores.Wisdom = new() { Value = value, IsProficient = isProficient };
        return new IntelligenceStage(_actor);
    }
}