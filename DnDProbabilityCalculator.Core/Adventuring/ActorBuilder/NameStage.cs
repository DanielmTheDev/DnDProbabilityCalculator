namespace DnDProbabilityCalculator.Core.Adventuring.ActorBuilder;

public class NameStage : INameStage
{
    private readonly Actor _actor;

    public NameStage(Actor actor)
        => _actor = actor;

    public IStrengthStage WithName(string value)
    {
        _actor.Name = value;
        return new StrengthStage(_actor);
    }
}