namespace DnDProbabilityCalculator.Core.Adventuring.ActorBuilder;

public class ConstitutionStage : IConstitutionStage
{
    private readonly Actor _actor;

    public ConstitutionStage(Actor actor)
        => _actor = actor;

    public IWisdomStage WithConstitution(int value, bool isProficient = false)
    {
        _actor.AbilityScores.Constitution = new() { Value = value, IsProficient = isProficient, Type = AbilityType.Constitution };
        return new WisdomStage(_actor);
    }
}