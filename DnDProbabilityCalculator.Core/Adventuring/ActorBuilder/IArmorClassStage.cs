namespace DnDProbabilityCalculator.Core.Adventuring.ActorBuilder;

public interface IArmorClassStage
{
    IAttackAbilityStage WithArmorClass(int armorClass);
}