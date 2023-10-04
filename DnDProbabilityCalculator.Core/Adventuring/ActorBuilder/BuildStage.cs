namespace DnDProbabilityCalculator.Core.Adventuring.ActorBuilder;

public class BuildStage : IBuildStage
{
    private readonly Actor _actor;

    public BuildStage(Actor actor)
        => _actor = actor;

    public Actor Build()
        => _actor;
}