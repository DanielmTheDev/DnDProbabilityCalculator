namespace DnDProbabilityCalculator.Core.Adventuring.ActorBuilder;

public interface IProficiencyStage
{
    IArmorClassStage WithProficiency(int proficiency);
}