namespace DnDProbabilityCalculator.Core.Adventuring.ActorBuilder;

public interface IArmorClassStage
{
    INumberOfAttacksStage WithArmorClass(int armorClass);
}