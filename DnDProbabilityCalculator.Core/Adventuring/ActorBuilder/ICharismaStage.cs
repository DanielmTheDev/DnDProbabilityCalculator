namespace DnDProbabilityCalculator.Core.Adventuring.ActorBuilder;

public interface ICharismaStage
{
    IBuildStage WithCharisma(int value, bool isProficient = false);
}