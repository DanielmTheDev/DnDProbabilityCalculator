namespace DnDProbabilityCalculator.Core.Adventuring.ActorBuilder;

public interface IConstitutionStage
{
    IWisdomStage WithConstitution(int value, bool isProficient = false, bool isAttackAbility = false);
}