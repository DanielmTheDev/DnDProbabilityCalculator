namespace DnDProbabilityCalculator.Core.Adventuring.ActorBuilder;

public interface INumberOfAttacksStage
{
    IAttackAbilityStage WithNumberOfAttacks(int numberOfAttacks);
}