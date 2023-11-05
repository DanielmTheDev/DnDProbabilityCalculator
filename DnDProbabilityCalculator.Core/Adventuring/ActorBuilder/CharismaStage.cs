using DnDProbabilityCalculator.Core.Adventuring.Abilities;

namespace DnDProbabilityCalculator.Core.Adventuring.ActorBuilder;

public class CharismaStage : ICharismaStage
{
    private readonly Actor _actor;

    public CharismaStage(Actor actor)
        => _actor = actor;

    public IProficiencyStage WithCharisma(int value, bool isProficient = false, bool isAttackAbility = false)
    {
        _actor.AbilityScores.Charisma = new Charisma { Value = value, IsProficient = isProficient, IsAttackAbility = isAttackAbility };
        return new ProficiencyStage(_actor);
    }
}