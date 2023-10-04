namespace DnDProbabilityCalculator.Core.Adventuring.ActorBuilder;

public interface IProficiencyStage
{
    IBuildStage WithProficiency(int proficiency);
}