namespace DnDProbabilityCalculator.Core.Adventuring.ActorBuilder;

public interface IDexterityStage
{
    IConstitutionStage WithDexterity(int value, bool isProficient = false);
}