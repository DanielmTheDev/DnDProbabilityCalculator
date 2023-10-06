namespace DnDProbabilityCalculator.Core.Adventuring.ActorBuilder;

public interface INameStage
{
    IStrengthStage WithName(string value);
}