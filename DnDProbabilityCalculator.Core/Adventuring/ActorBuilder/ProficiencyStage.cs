namespace DnDProbabilityCalculator.Core.Adventuring.ActorBuilder;

public class ProficiencyStage : IProficiencyStage
{
    private readonly Actor _actor;

    public ProficiencyStage(Actor actor)
        => _actor = actor;

    public IBuildStage WithProficiency(int proficiency)
    {
        _actor.ProficiencyBonus = proficiency;
        return new BuildStage(_actor);
    }
}