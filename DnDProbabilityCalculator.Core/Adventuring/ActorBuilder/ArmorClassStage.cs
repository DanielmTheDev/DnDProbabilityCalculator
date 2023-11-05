namespace DnDProbabilityCalculator.Core.Adventuring.ActorBuilder;

public class ArmorClassStage : IArmorClassStage
{
    private readonly Actor _actor;

    public ArmorClassStage(Actor actor)
        => _actor = actor;

    public INumberOfAttacksStage WithArmorClass(int armorClass)
    {
        _actor.ArmorClass = armorClass;
        return new NumberOfAttacksStage(_actor);
    }
}