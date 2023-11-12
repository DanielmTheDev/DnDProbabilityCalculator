using DnDProbabilityCalculator.Core.Adventuring;

namespace DnDProbabilityCalculator.Application.Table;

public record TableContext
{
    private TableContext()
    {
    }

    public required string ActorName { get; init; }
    public required SavingThrowTable SavingThrowTable { get; init; }
    public required ReceiveHitTable ReceiveHitTable { get; init; }
    public required DeliverHitTable DeliverHitTable { get; init; }

    public static TableContext FromActor(Actor actor, InputVariables inputVariables)
    {
        return new()
        {
            ActorName = actor.Name,
            SavingThrowTable = SavingThrowTable.FromActor(actor, inputVariables.Dcs),
            ReceiveHitTable = ReceiveHitTable.FromActor(actor, inputVariables.AttackModifiers, inputVariables.NumberOfAttacks),
            DeliverHitTable = DeliverHitTable.FromActor(actor, inputVariables.ArmorClasses)
        };
    }
}