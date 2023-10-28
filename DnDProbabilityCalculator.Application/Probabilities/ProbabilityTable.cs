using DnDProbabilityCalculator.Core.Adventuring;

namespace DnDProbabilityCalculator.Application.Probabilities;

public record ProbabilityTable
{
    private ProbabilityTable() { }

    public required string ActorName { get; init; }
    public required SavingThrowTable SavingThrowTable { get; set; }
    public required GetHitTable GetHitTable { get; set; }

    public static ProbabilityTable FromActor(Actor actor, int[] dcs, int[] attackModifiers)
    {
        ValidateSameNumberOfElements(dcs, attackModifiers);
        return new()
        {
            ActorName = actor.Name,
            SavingThrowTable = SavingThrowTable.FromActor(actor, dcs),
            GetHitTable = GetHitTable.FromActor(actor, attackModifiers, 3)
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