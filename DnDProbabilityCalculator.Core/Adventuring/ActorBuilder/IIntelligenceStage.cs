namespace DnDProbabilityCalculator.Core.Adventuring.ActorBuilder;

public interface IIntelligenceStage
{
    ICharismaStage WithIntelligence(int value, bool isProficient = false, bool isAttackAbility = false);
}