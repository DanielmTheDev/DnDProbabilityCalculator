using DnDProbabilityCalculator.Core.Adventuring;

namespace DnDProbabilityCalculator.Application.Table.Context;

public class DeliverHitTable
{
    public required int TotalNumberOfAttacks { get; init; }
    public required List<int> ArmorClasses { get; init; }
    public required List<DeliverHitTableRow> Probabilities { get; init; }

    private DeliverHitTable()
    {
    }

    public static DeliverHitTable FromActor(Actor actor, InputVariables inputVariables)
    {
        var probabilityRows = Enumerable.Range(1, actor.NumberOfAttacks)
            .Select(currentNumberOfHits => CreateDeliverHitRow(actor, inputVariables, currentNumberOfHits))
            .ToList();

        return new()
        {
            TotalNumberOfAttacks = actor.NumberOfAttacks,
            ArmorClasses = inputVariables.ArmorClasses.ToList(),
            Probabilities = probabilityRows
        };
    }

    private static DeliverHitTableRow CreateDeliverHitRow(Actor actor, InputVariables inputVariables, int currentNumberOfHits)
    {
        var probabilities = inputVariables.ArmorClasses
            .Select(currentArmorClass => actor.DeliverHitChance(currentArmorClass, currentNumberOfHits, inputVariables.Advantage).Probability)
            .ToList();
        return new()
        {
            NumberOfHits = currentNumberOfHits,
            Cells = probabilities
        };
    }
}

public record DeliverHitTableRow
{
    public required int NumberOfHits { get; init; }
    public required List<double> Cells { get; init; }
}