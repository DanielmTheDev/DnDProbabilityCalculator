namespace DnDProbabilityCalculator.Core.Adventuring.ActorBuilder;

public class CharismaStage : ICharismaStage
{
    private readonly Actor _actor;

    public CharismaStage(Actor actor)
        => _actor = actor;

    public IProficiencyStage WithCharisma(int value, bool isProficient = false)
    {
        _actor.AbilityScores.Charisma = new AbilityScore { Value = value, IsProficient = isProficient, Type = AbilityType.Charisma };
        return new ProficiencyStage(_actor);
    }
}