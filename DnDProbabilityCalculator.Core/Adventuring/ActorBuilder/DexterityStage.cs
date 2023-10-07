namespace DnDProbabilityCalculator.Core.Adventuring.ActorBuilder;

public class DexterityStage : IDexterityStage
{
    private readonly Actor _actor;

    public DexterityStage(Actor actor)
        => _actor = actor;

    public IConstitutionStage WithDexterity(int value, bool isProficient = false)
    {
        _actor.AbilityScores.Dexterity = new() { Value = value, IsProficient = isProficient };
        return new ConstitutionStage(_actor);
    }
}