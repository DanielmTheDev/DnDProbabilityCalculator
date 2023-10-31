using DnDProbabilityCalculator.Core.Adventuring;

namespace DnDProbabilityCalculator.Application.Probabilities;

public record ProbabilityTable
{
    private ProbabilityTable() { }

    public required string ActorName { get; init; }
    public required SavingThrowTable SavingThrowTable { get; init; }
    public required GetHitTable GetHitTable { get; init; }

    public static ProbabilityTable FromActor(Actor actor, InputVariables inputVariables)
    {
        return new()
        {
            ActorName = actor.Name,
            SavingThrowTable = SavingThrowTable.FromActor(actor, inputVariables.Dcs),
            GetHitTable = GetHitTable.FromActor(actor, inputVariables.AttackModifiers, inputVariables.NumberOfAttacks)
        };
    }
}