namespace DnDProbabilityCalculator.Core.Adventuring.ActorBuilder;

public class BuildStage : IBuildStage
{
    private readonly Actor _actor;

    public BuildStage(Actor actor)
        => _actor = actor;

    public Actor Build()
    {
        var numberOfAttackAbilities = _actor.AbilityScores.AsList().Count(score => score.IsAttackAbility);
        if (numberOfAttackAbilities > 1)
        {
            throw new InvalidOperationException(ErrorMessages.More_Than_One_Attack_Ability);
        }
        return _actor;
    }
}