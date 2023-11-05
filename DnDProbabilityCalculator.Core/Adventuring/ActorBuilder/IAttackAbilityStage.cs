using DnDProbabilityCalculator.Core.Adventuring.Abilities;

namespace DnDProbabilityCalculator.Core.Adventuring.ActorBuilder;

public interface IAttackAbilityStage
{
    IBuildStage WithAttackAbility(AbilityScoreType abilityScoreType);
}