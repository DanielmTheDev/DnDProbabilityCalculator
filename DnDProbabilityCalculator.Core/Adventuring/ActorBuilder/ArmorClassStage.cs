using DnDProbabilityCalculator.Core.Adventuring.Abilities;

namespace DnDProbabilityCalculator.Core.Adventuring.ActorBuilder;

public class ArmorClassStage : IArmorClassStage
{
    private readonly Actor _actor;

    public ArmorClassStage(Actor actor)
        => _actor = actor;

    public IAttackAbilityStage WithArmorClass(int armorClass)
    {
        _actor.ArmorClass = armorClass;
        return new AttackAbilitStage(_actor);
    }
}

public class AttackAbilitStage : IAttackAbilityStage
{
    private readonly Actor _actor;

    public AttackAbilitStage(Actor actor)
        => _actor = actor;

    public IBuildStage WithAttackAbility(AbilityScoreType abilityScoreType)
    {
        _actor.AttackAbility = abilityScoreType;
        return new BuildStage(_actor);
    }
}