using DnDProbabilityCalculator.Core.Adventuring;

namespace DnDProbabilityCalculator.Application.Table.Context;

public class ReceiveHitTable
{
    public required List<int> AttackModifiers { get; init; }
    public required List<ReceiveHitTableRow> Probabilities { get; init; }
    public required int TotalNumberOfAttacks { get; set; }

    private ReceiveHitTable()
    {
    }

    public static ReceiveHitTable FromActor(Actor actor, InputVariables inputVariables)
    {
        var probabilityRows = Enumerable.Range(1, inputVariables.NumberOfAttacks)
            .Select(currentNumberOfHits => CreateGetHitRow(actor, inputVariables, currentNumberOfHits))
            .ToList();

        return new()
        {
            AttackModifiers = inputVariables.AttackModifiers.ToList(),
            Probabilities = probabilityRows,
            TotalNumberOfAttacks = inputVariables.NumberOfAttacks
        };
    }

    private static ReceiveHitTableRow CreateGetHitRow(Actor actor, InputVariables inputVariables, int currentNumberOfHits)
    {
        var probabilities = inputVariables.AttackModifiers
            .Select(currentModifier => actor.ReceiveHitChance(currentModifier, inputVariables.NumberOfAttacks, currentNumberOfHits, inputVariables.Advantage).Probability)
            .ToList();
        return new()
        {
            NumberOfHits = currentNumberOfHits,
            Cells = probabilities
        };
    }
}

public record ReceiveHitTableRow
{
    public required int NumberOfHits { get; init; }
    public required List<double> Cells { get; init; }
}