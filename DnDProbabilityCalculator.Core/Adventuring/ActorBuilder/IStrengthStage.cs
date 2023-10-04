namespace DnDProbabilityCalculator.Core.Adventuring.ActorBuilder;

public interface IStrengthStage
{
    IDexterityStage WithStrength(int value, bool isProficient = false);
}