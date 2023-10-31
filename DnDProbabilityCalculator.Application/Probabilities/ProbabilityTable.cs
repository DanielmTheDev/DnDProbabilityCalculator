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
        ValidateSameNumberOfElements(inputVariables.Dcs, inputVariables.AttackModifiers); // todo: valdation in inputVariables model
        return new()
        {
            ActorName = actor.Name,
            SavingThrowTable = SavingThrowTable.FromActor(actor, inputVariables.Dcs),
            GetHitTable = GetHitTable.FromActor(actor, inputVariables.AttackModifiers, inputVariables.NumberOfAttacks)
        };
    }

    private static void ValidateSameNumberOfElements(int[] dcs, int[] attackModifiers)
    {
        if (dcs.Length != attackModifiers.Length)
        {
            throw new ArgumentException("The number of dcs and attack modifiers must be the same.");
        }
    }
}