namespace DnDProbabilityCalculator.Core.Adventuring.ActorBuilder;

public class DexterityStage : IDexterityStage
{
    private readonly Actor _actor;

    public DexterityStage(Actor actor)
        => _actor = actor;

    public IConstitutionStage WithDexterity(int value, bool isProficient = false, bool isAttackAbility = false)
    {
        _actor.AbilityScores.Dexterity = new() { Value = value, IsProficient = isProficient, IsAttackAbility = isAttackAbility };
        return new ConstitutionStage(_actor);
    }
}