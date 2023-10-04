namespace DnDProbabilityCalculator.Core.Adventuring.ActorBuilder;

public interface ICharismaStage
{
    IProficiencyStage WithCharisma(int value, bool isProficient = false);
}