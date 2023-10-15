namespace DnDProbabilityCalculator.Core.Adventuring.ActorBuilder;

public class ArmorClassStage : IArmorClassStage
{
    private readonly Actor _actor;

    public ArmorClassStage(Actor actor)
        => _actor = actor;

    public IBuildStage WithArmorClass(int armorClass)
    {
        _actor.ArmorClass = armorClass;
        return new BuildStage(_actor);
    }
}