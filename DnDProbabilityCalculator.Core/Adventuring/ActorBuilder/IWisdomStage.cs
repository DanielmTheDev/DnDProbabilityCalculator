namespace DnDProbabilityCalculator.Core.Adventuring.ActorBuilder;

public interface IWisdomStage
{
    IIntelligenceStage WithWisdom(int value, bool isProficient = false);
}