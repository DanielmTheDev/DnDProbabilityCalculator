namespace DnDProbabilityCalculator.Core.Adventuring.ActorBuilder;

public class NumberOfAttacksStage : INumberOfAttacksStage
{
    private readonly Actor _actor;


    public NumberOfAttacksStage(Actor actor)
    {
        _actor = actor;
    }

    public IAttackAbilityStage WithNumberOfAttacks(int numberOfAttacks)
    {
        _actor.NumberOfAttacks = numberOfAttacks;
        return new AttackAbilityStage(_actor);
    }
}